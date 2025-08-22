using UnityEngine;

public enum Rank
{
    [InspectorName("F-")] FMinus,
    F,
    [InspectorName("F+")] FPlus,
    [InspectorName("D-")] DMinus,
    D,
    [InspectorName("D+")] DPlus,
    [InspectorName("C-")] CMinus,
    C,
    [InspectorName("C+")] CPlus,
    [InspectorName("B-")] BMinus,
    B,
    [InspectorName("B+")] BPlus,
    [InspectorName("A-")] AMinus,
    A,
    [InspectorName("A+")] APlus,
    [InspectorName("S-")] SMinus,
    S,
    [InspectorName("S+")] SPlus,
    [InspectorName("SS-")] SSMinus,
    SS,
    [InspectorName("SS+")] SSPlus,
    [InspectorName("SSS-")] SSSMinus,
    SSS,
    [InspectorName("SSS+")] SSSPlus
}
