using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200003F RID: 63
	[DataContract(IsReference = true)]
	public class DataSourceFunctionParameter
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000037FE File Offset: 0x000019FE
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00003806 File Offset: 0x00001A06
		[DataMember(Name = "name", Order = 0)]
		public string Name { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000380F File Offset: 0x00001A0F
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00003817 File Offset: 0x00001A17
		[DataMember(Name = "parameterType", Order = 10)]
		public string ParameterType { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00003820 File Offset: 0x00001A20
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00003828 File Offset: 0x00001A28
		[DataMember(Name = "isRequired", Order = 20)]
		public bool IsRequired { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00003831 File Offset: 0x00001A31
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00003839 File Offset: 0x00001A39
		[DataMember(Name = "isNullable", Order = 30)]
		public bool IsNullable { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00003842 File Offset: 0x00001A42
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000384A File Offset: 0x00001A4A
		[DataMember(Name = "fieldCaption", Order = 40)]
		public string FieldCaption { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00003853 File Offset: 0x00001A53
		// (set) Token: 0x060001AD RID: 429 RVA: 0x0000385B File Offset: 0x00001A5B
		[DataMember(Name = "fieldDescription", Order = 50)]
		public string FieldDescription { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00003864 File Offset: 0x00001A64
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000386C File Offset: 0x00001A6C
		[DataMember(Name = "sampleValues", Order = 60)]
		public IList<string> SampleValues { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00003875 File Offset: 0x00001A75
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000387D File Offset: 0x00001A7D
		[DataMember(Name = "allowedValues", Order = 70)]
		public IList<string> AllowedValues { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00003886 File Offset: 0x00001A86
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000388E File Offset: 0x00001A8E
		[DataMember(Name = "defaultValue", Order = 80)]
		public string DefaultValue { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00003897 File Offset: 0x00001A97
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000389F File Offset: 0x00001A9F
		[DataMember(Name = "enumNames", Order = 90, IsRequired = false)]
		public IList<string> EnumNames { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000038A8 File Offset: 0x00001AA8
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000038B0 File Offset: 0x00001AB0
		[DataMember(Name = "enumCaptions", Order = 100, IsRequired = false)]
		public IList<string> EnumCaptions { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000038B9 File Offset: 0x00001AB9
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x000038C1 File Offset: 0x00001AC1
		[DataMember(Name = "recordParameters", Order = 110, IsRequired = false)]
		public IList<DataSourceFunctionParameter> RecordParameters { get; set; }
	}
}
