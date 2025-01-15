using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts.Model
{
	// Token: 0x02000026 RID: 38
	[DataContract]
	internal sealed class ModelTelemetry
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000038B8 File Offset: 0x00001AB8
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x000038C0 File Offset: 0x00001AC0
		[DataMember(Name = "Caption", EmitDefaultValue = false, Order = 10)]
		public string Caption { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000038C9 File Offset: 0x00001AC9
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000038D1 File Offset: 0x00001AD1
		[DataMember(Name = "Entities", EmitDefaultValue = false, Order = 20)]
		public int EntityCount { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000038DA File Offset: 0x00001ADA
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000038E2 File Offset: 0x00001AE2
		[DataMember(Name = "EntitiesNameLength", EmitDefaultValue = false, Order = 30)]
		public int EntityNameTotalLength { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000038EB File Offset: 0x00001AEB
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000038F3 File Offset: 0x00001AF3
		[DataMember(Name = "Props", EmitDefaultValue = false, Order = 40)]
		public int PropertyCount { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000038FC File Offset: 0x00001AFC
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00003904 File Offset: 0x00001B04
		[DataMember(Name = "PropsNameLength", EmitDefaultValue = false, Order = 50)]
		public int PropertyNameTotalLength { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000390D File Offset: 0x00001B0D
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00003915 File Offset: 0x00001B15
		[DataMember(Name = "Rel1To1", EmitDefaultValue = false, Order = 60)]
		public int Relationships1to1 { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000391E File Offset: 0x00001B1E
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00003926 File Offset: 0x00001B26
		[DataMember(Name = "Rel1ToM", EmitDefaultValue = false, Order = 70)]
		public int Relationships1toM { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000392F File Offset: 0x00001B2F
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00003937 File Offset: 0x00001B37
		[DataMember(Name = "RelMToM", EmitDefaultValue = false, Order = 80)]
		public int RelationshipsMtoM { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003940 File Offset: 0x00001B40
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00003948 File Offset: 0x00001B48
		[DataMember(Name = "Kind", EmitDefaultValue = true, Order = 90)]
		public string Kind { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003951 File Offset: 0x00001B51
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00003959 File Offset: 0x00001B59
		[DataMember(Name = "RelNormalized", EmitDefaultValue = false, Order = 100)]
		public int NormalizedRelationshipCount { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003962 File Offset: 0x00001B62
		// (set) Token: 0x060000EB RID: 235 RVA: 0x0000396A File Offset: 0x00001B6A
		[DataMember(Name = "DegenerateDims", EmitDefaultValue = false, Order = 110)]
		public int DegenerateDimensionCount { get; set; }
	}
}
