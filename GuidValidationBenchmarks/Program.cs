using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using GuidTestBenchmarks;

var config = DefaultConfig.Instance.AddJob(
    Job.Default.DontEnforcePowerPlan().WithToolchain(InProcessEmitToolchain.Instance)
);

BenchmarkRunner.Run<RegexVsGuidParse>(config);