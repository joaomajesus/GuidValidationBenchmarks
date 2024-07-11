using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace GuidTestBenchmarks
{
    [MemoryDiagnoser]
    [RPlotExporter]
    [SuppressMessage(
        "Major Code Smell",
        "S1118:Utility classes should not have public constructors",
        Justification = "BenchmarkDotNet doesn't support static or protected classes"
    )]
    public partial class RegexVsGuidParse
    {
        private const string pattern = @"/\w+/\b[A-F0-9]{8}(?:-[A-F0-9]{4}){3}-[A-F0-9]{12}\b";

        private static readonly Regex compiledRegex =
            new(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static string ValueWithGuid => $"/prefix/{Guid.NewGuid()}";
        private static string ValueWithoutGuid => "/prefix/";

        private static readonly List<Func<string>> generators =
            new() { () => ValueWithGuid, () => ValueWithoutGuid };

        private static readonly IEnumerable<string> data = Enumerable
            .Range(0, 10_000)
            .Select(_ => generators[RandomNumberGenerator.GetInt32(0, 2)]());

        [GeneratedRegex(pattern, RegexOptions.IgnoreCase)]
        private static partial Regex GetSourceGeneratedRegex();

        [Benchmark]
        public static void GuidTryParseWithSplit()
        {
            foreach (var value in data)
            {
                var split = value.Split('/');

                if (split.Length == 3)
                    Guid.TryParse(split[2], out var _);
            }
        }

        [Benchmark]
        public static void GuidParseWithSplitRemoveEmptyEntries()
        {
            foreach (var value in data)
            {
                var split = value.Split('/', StringSplitOptions.RemoveEmptyEntries);

                if (split.Length == 2)
                    Guid.Parse(split[1]);
            }
        }

        [Benchmark]
        public static void SourceGeneratedRegex()
        {
            var regex = GetSourceGeneratedRegex();

            foreach (var value in data)
                regex.IsMatch(value);
        }

        [Benchmark]
        public static void CompiledRegex()
        {
            foreach (var value in data)
                compiledRegex.IsMatch(value);
        }
    }
}
