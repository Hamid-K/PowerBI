using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000A0 RID: 160
	[DataContract(Name = "InterpretRequest", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class InterpretRequest
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00006686 File Offset: 0x00004886
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000668E File Offset: 0x0000488E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public int Version { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00006697 File Offset: 0x00004897
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000669F File Offset: 0x0000489F
		[DataMember(IsRequired = true, Order = 20)]
		public string Utterance { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000066A8 File Offset: 0x000048A8
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x000066B0 File Offset: 0x000048B0
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string Language { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x000066B9 File Offset: 0x000048B9
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x000066C1 File Offset: 0x000048C1
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public DateTime? ClientTime { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x000066CA File Offset: 0x000048CA
		// (set) Token: 0x060002FA RID: 762 RVA: 0x000066D2 File Offset: 0x000048D2
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public InterpretRequestOptions Options { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000066DB File Offset: 0x000048DB
		// (set) Token: 0x060002FC RID: 764 RVA: 0x000066E3 File Offset: 0x000048E3
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public int DesiredInterpretationsCount { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000066EC File Offset: 0x000048EC
		// (set) Token: 0x060002FE RID: 766 RVA: 0x000066F4 File Offset: 0x000048F4
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 80)]
		public DependentSchema[] DependentSchemas { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000066FD File Offset: 0x000048FD
		// (set) Token: 0x06000300 RID: 768 RVA: 0x00006705 File Offset: 0x00004905
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 90)]
		public UserInfo UserInfo { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000670E File Offset: 0x0000490E
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00006716 File Offset: 0x00004916
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 100)]
		public double MinimumScore { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000671F File Offset: 0x0000491F
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00006727 File Offset: 0x00004927
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 110)]
		public IList<Term> Terms { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00006730 File Offset: 0x00004930
		// (set) Token: 0x06000306 RID: 774 RVA: 0x00006738 File Offset: 0x00004938
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 120)]
		public ConversationalContext ConversationalContext { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00006741 File Offset: 0x00004941
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00006749 File Offset: 0x00004949
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 130)]
		public int? SuggestedUtteranceRandomSeed { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00006752 File Offset: 0x00004952
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000675A File Offset: 0x0000495A
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 140)]
		public int? DesiredSuggestedUtteranceCount { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00006763 File Offset: 0x00004963
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000676B File Offset: 0x0000496B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 150)]
		public IList<FeatureSwitch> FeatureSwitches { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00006774 File Offset: 0x00004974
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000677C File Offset: 0x0000497C
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 160)]
		public ResultConfidenceLevel? MinResultConfidence { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00006785 File Offset: 0x00004985
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000678D File Offset: 0x0000498D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 170)]
		public string LinguisticSchemaJson { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00006796 File Offset: 0x00004996
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000679E File Offset: 0x0000499E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 180)]
		public InferredTermBinding InferredTermBinding { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000313 RID: 787 RVA: 0x000067A7 File Offset: 0x000049A7
		// (set) Token: 0x06000314 RID: 788 RVA: 0x000067AF File Offset: 0x000049AF
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 190)]
		public bool SkipCompletion { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000315 RID: 789 RVA: 0x000067B8 File Offset: 0x000049B8
		// (set) Token: 0x06000316 RID: 790 RVA: 0x000067C0 File Offset: 0x000049C0
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 200)]
		public Dictionary<string, SchemaAddition> SchemaAdditions { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000317 RID: 791 RVA: 0x000067C9 File Offset: 0x000049C9
		// (set) Token: 0x06000318 RID: 792 RVA: 0x000067D1 File Offset: 0x000049D1
		internal ImmutableList<string> AncestorTextualEntityNames { get; set; }

		// Token: 0x06000319 RID: 793 RVA: 0x000067DA File Offset: 0x000049DA
		public static InterpretRequest FromXmlString(string xml)
		{
			return InterpretRequest._serializer.FromXmlString(xml);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000067E7 File Offset: 0x000049E7
		public string ToXmlString()
		{
			return InterpretRequest._serializer.ToXmlString(this, false);
		}

		// Token: 0x04000359 RID: 857
		private static readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(InterpretRequest));
	}
}
