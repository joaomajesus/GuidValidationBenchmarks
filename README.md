# Results

| Method                               | Mean     | Error     | StdDev    | Gen0     | Allocated  |
|------------------------------------- |---------:|----------:|----------:|---------:|-----------:|
| GuidTryParseWithSplit                | 1.616 ms | 0.0309 ms | 0.0517 ms | 113.2813 | 1875.44 KB |
| GuidTryParseWithSplitLastOrDefault   | 1.746 ms | 0.0346 ms | 0.0632 ms | 113.2813 | 1874.31 KB |
| GuidParseWithSplitRemoveEmptyEntries | 1.638 ms | 0.0322 ms | 0.0581 ms | 134.7656 | 2226.49 KB |
| SourceGeneratedRegex                 | 1.325 ms | 0.0260 ms | 0.0365 ms |  33.2031 |  547.02 KB |
| CompiledRegex                        | 1.407 ms | 0.0272 ms | 0.0407 ms |  33.2031 |  547.34 KB |