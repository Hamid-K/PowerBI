using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.InternalContracts.DataShapeDefinition
{
	// Token: 0x0200011E RID: 286
	[DataContract]
	internal sealed class DataShape
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0000F909 File Offset: 0x0000DB09
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x0000F911 File Offset: 0x0000DB11
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Id { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0000F91A File Offset: 0x0000DB1A
		// (set) Token: 0x060007A4 RID: 1956 RVA: 0x0000F922 File Offset: 0x0000DB22
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal IList<Calculation> Calculations { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0000F92B File Offset: 0x0000DB2B
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x0000F933 File Offset: 0x0000DB33
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal IList<DataMember> SecondaryHierarchy { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x0000F93C File Offset: 0x0000DB3C
		// (set) Token: 0x060007A8 RID: 1960 RVA: 0x0000F944 File Offset: 0x0000DB44
		[DataMember(EmitDefaultValue = false, Order = 4)]
		internal IList<DataMember> PrimaryHierarchy { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0000F94D File Offset: 0x0000DB4D
		// (set) Token: 0x060007AA RID: 1962 RVA: 0x0000F955 File Offset: 0x0000DB55
		[DataMember(EmitDefaultValue = false, Order = 5)]
		internal IList<DataShape> DataShapes { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0000F95E File Offset: 0x0000DB5E
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x0000F966 File Offset: 0x0000DB66
		[DataMember(EmitDefaultValue = false, Order = 6)]
		internal DataBinding DataBinding { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0000F96F File Offset: 0x0000DB6F
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x0000F977 File Offset: 0x0000DB77
		[DataMember(EmitDefaultValue = false, Order = 7)]
		internal DataWindow DataWindow { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0000F980 File Offset: 0x0000DB80
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x0000F988 File Offset: 0x0000DB88
		[DataMember(EmitDefaultValue = false, Order = 8)]
		internal DataLimits DataLimits { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0000F991 File Offset: 0x0000DB91
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x0000F999 File Offset: 0x0000DB99
		[DataMember(EmitDefaultValue = false, Order = 9)]
		internal FieldValueExpressionNode CorrelationExpression { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0000F9A2 File Offset: 0x0000DBA2
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x0000F9AA File Offset: 0x0000DBAA
		[DataMember(EmitDefaultValue = false, Order = 10)]
		internal IList<Message> Messages { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0000F9B3 File Offset: 0x0000DBB3
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0000F9BB File Offset: 0x0000DBBB
		[DataMember(EmitDefaultValue = false, Order = 11)]
		internal bool HasReusableSecondary { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x0000F9CC File Offset: 0x0000DBCC
		[DataMember(EmitDefaultValue = false, Order = 12)]
		internal IList<IList<ExpressionNode>> RestartDefinitions { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0000F9D5 File Offset: 0x0000DBD5
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0000F9DD File Offset: 0x0000DBDD
		[DataMember(EmitDefaultValue = false, Order = 13)]
		internal IList<string> SegmentationTableIds { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0000F9E6 File Offset: 0x0000DBE6
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x0000F9EE File Offset: 0x0000DBEE
		[DataMember(EmitDefaultValue = false, Order = 14)]
		internal CorrelationMode CorrelationMode { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0000F9F7 File Offset: 0x0000DBF7
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0000F9FF File Offset: 0x0000DBFF
		[DataMember(EmitDefaultValue = false, Order = 15)]
		internal DataWindows DataWindows { get; set; }
	}
}
