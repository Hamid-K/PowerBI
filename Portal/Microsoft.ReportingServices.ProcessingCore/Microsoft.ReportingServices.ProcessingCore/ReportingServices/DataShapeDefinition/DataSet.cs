using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportProcessing.Utilities;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x0200058B RID: 1419
	[DataContract]
	internal sealed class DataSet
	{
		// Token: 0x0600518C RID: 20876 RVA: 0x00159BE0 File Offset: 0x00157DE0
		internal DataSet(string id)
		{
			this.m_id = id;
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x00159BF0 File Offset: 0x00157DF0
		internal DataSet(string id, Query query, IEnumerable<Field> fields, string collationCulture, DataSet.TriState caseSensitivity, DataSet.TriState accentSensitivity, DataSet.TriState kanatypeSensitivity, DataSet.TriState widthSensitivity, DataSet.TriState interpretSubtotalsAsDetails, bool nullsAsBlanks, bool useOrdinalStringKeyGeneration, IEnumerable<DefaultRelationship> defaultRelationships)
			: this(id)
		{
			this.m_query = query;
			this.m_fields = fields.ToReadOnlyCollection<Field>();
			this.m_collationCulture = collationCulture;
			this.m_caseSensitivity = caseSensitivity;
			this.m_accentSensitivity = accentSensitivity;
			this.m_kanatypeSensitivity = kanatypeSensitivity;
			this.m_widthSensitivity = widthSensitivity;
			this.m_interpretSubtotalsAsDetails = interpretSubtotalsAsDetails;
			this.m_nullsAsBlanks = nullsAsBlanks;
			this.m_useOrdinalStringKeyGeneration = useOrdinalStringKeyGeneration;
			this.m_defaultRelationships = defaultRelationships;
		}

		// Token: 0x17001E41 RID: 7745
		// (get) Token: 0x0600518E RID: 20878 RVA: 0x00159C5F File Offset: 0x00157E5F
		internal string ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x17001E42 RID: 7746
		// (get) Token: 0x0600518F RID: 20879 RVA: 0x00159C67 File Offset: 0x00157E67
		internal IEnumerable<Field> Fields
		{
			get
			{
				return this.m_fields;
			}
		}

		// Token: 0x17001E43 RID: 7747
		// (get) Token: 0x06005190 RID: 20880 RVA: 0x00159C6F File Offset: 0x00157E6F
		internal Query Query
		{
			get
			{
				return this.m_query;
			}
		}

		// Token: 0x17001E44 RID: 7748
		// (get) Token: 0x06005191 RID: 20881 RVA: 0x00159C77 File Offset: 0x00157E77
		internal string CollationCulture
		{
			get
			{
				return this.m_collationCulture;
			}
		}

		// Token: 0x17001E45 RID: 7749
		// (get) Token: 0x06005192 RID: 20882 RVA: 0x00159C7F File Offset: 0x00157E7F
		internal DataSet.TriState CaseSensitivity
		{
			get
			{
				return this.m_caseSensitivity;
			}
		}

		// Token: 0x17001E46 RID: 7750
		// (get) Token: 0x06005193 RID: 20883 RVA: 0x00159C87 File Offset: 0x00157E87
		internal DataSet.TriState AccentSensitivity
		{
			get
			{
				return this.m_accentSensitivity;
			}
		}

		// Token: 0x17001E47 RID: 7751
		// (get) Token: 0x06005194 RID: 20884 RVA: 0x00159C8F File Offset: 0x00157E8F
		internal DataSet.TriState KanatypeSensitivity
		{
			get
			{
				return this.m_kanatypeSensitivity;
			}
		}

		// Token: 0x17001E48 RID: 7752
		// (get) Token: 0x06005195 RID: 20885 RVA: 0x00159C97 File Offset: 0x00157E97
		internal DataSet.TriState WidthSensitivity
		{
			get
			{
				return this.m_widthSensitivity;
			}
		}

		// Token: 0x17001E49 RID: 7753
		// (get) Token: 0x06005196 RID: 20886 RVA: 0x00159C9F File Offset: 0x00157E9F
		internal DataSet.TriState InterpretSubtotalsAsDetails
		{
			get
			{
				return this.m_interpretSubtotalsAsDetails;
			}
		}

		// Token: 0x17001E4A RID: 7754
		// (get) Token: 0x06005197 RID: 20887 RVA: 0x00159CA7 File Offset: 0x00157EA7
		internal bool NullsAsBlanks
		{
			get
			{
				return this.m_nullsAsBlanks;
			}
		}

		// Token: 0x17001E4B RID: 7755
		// (get) Token: 0x06005198 RID: 20888 RVA: 0x00159CAF File Offset: 0x00157EAF
		internal bool UseOrdinalStringKeyGeneration
		{
			get
			{
				return this.m_useOrdinalStringKeyGeneration;
			}
		}

		// Token: 0x17001E4C RID: 7756
		// (get) Token: 0x06005199 RID: 20889 RVA: 0x00159CB7 File Offset: 0x00157EB7
		internal IEnumerable<DefaultRelationship> DefaultRelationships
		{
			get
			{
				return this.m_defaultRelationships;
			}
		}

		// Token: 0x04002924 RID: 10532
		[DataMember(Name = "ID", Order = 1)]
		private readonly string m_id;

		// Token: 0x04002925 RID: 10533
		[DataMember(Name = "Query", Order = 2)]
		private readonly Query m_query;

		// Token: 0x04002926 RID: 10534
		[DataMember(Name = "Fields", Order = 3)]
		private readonly IEnumerable<Field> m_fields;

		// Token: 0x04002927 RID: 10535
		[DataMember(Name = "CollationCulture", Order = 4)]
		private readonly string m_collationCulture;

		// Token: 0x04002928 RID: 10536
		[DataMember(Name = "CaseSensitivity", Order = 5)]
		private readonly DataSet.TriState m_caseSensitivity;

		// Token: 0x04002929 RID: 10537
		[DataMember(Name = "AccentSensitivity", Order = 6)]
		private readonly DataSet.TriState m_accentSensitivity;

		// Token: 0x0400292A RID: 10538
		[DataMember(Name = "KanaTypeSensitivity", Order = 7)]
		private readonly DataSet.TriState m_kanatypeSensitivity;

		// Token: 0x0400292B RID: 10539
		[DataMember(Name = "WidthSensitivity", Order = 8)]
		private readonly DataSet.TriState m_widthSensitivity;

		// Token: 0x0400292C RID: 10540
		[DataMember(Name = "SubtotalsAsDetails", Order = 9)]
		private readonly DataSet.TriState m_interpretSubtotalsAsDetails;

		// Token: 0x0400292D RID: 10541
		[DataMember(Name = "NullsAsBlanks", Order = 10)]
		private readonly bool m_nullsAsBlanks;

		// Token: 0x0400292E RID: 10542
		[DataMember(Name = "UseOrdinalStringKeyGeneration", Order = 11)]
		private readonly bool m_useOrdinalStringKeyGeneration;

		// Token: 0x0400292F RID: 10543
		[DataMember(Name = "DefaultRelationships", Order = 12, EmitDefaultValue = false)]
		private readonly IEnumerable<DefaultRelationship> m_defaultRelationships;

		// Token: 0x02000C02 RID: 3074
		internal enum TriState
		{
			// Token: 0x040047EB RID: 18411
			Auto,
			// Token: 0x040047EC RID: 18412
			True,
			// Token: 0x040047ED RID: 18413
			False
		}
	}
}
