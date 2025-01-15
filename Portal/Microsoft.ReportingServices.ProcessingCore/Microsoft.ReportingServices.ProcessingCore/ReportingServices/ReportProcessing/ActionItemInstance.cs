using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000720 RID: 1824
	[Serializable]
	internal sealed class ActionItemInstance
	{
		// Token: 0x060065C5 RID: 26053 RVA: 0x00190274 File Offset: 0x0018E474
		internal ActionItemInstance(ReportProcessing.ProcessingContext pc, ActionItem actionItemDef)
		{
			ParameterValueList drillthroughParameters = actionItemDef.DrillthroughParameters;
			if (drillthroughParameters != null)
			{
				this.m_drillthroughParametersValues = new object[drillthroughParameters.Count];
				this.m_drillthroughParametersOmits = new BoolList(drillthroughParameters.Count);
				this.m_dataSetTokenIDs = new IntList(drillthroughParameters.Count);
				for (int i = 0; i < drillthroughParameters.Count; i++)
				{
					if (drillthroughParameters[i].Value != null && drillthroughParameters[i].Value.Type == ExpressionInfo.Types.Token)
					{
						this.m_dataSetTokenIDs.Add(drillthroughParameters[i].Value.IntValue);
					}
					else
					{
						this.m_dataSetTokenIDs.Add(-1);
					}
				}
			}
		}

		// Token: 0x060065C6 RID: 26054 RVA: 0x00190330 File Offset: 0x0018E530
		internal ActionItemInstance()
		{
		}

		// Token: 0x17002406 RID: 9222
		// (get) Token: 0x060065C7 RID: 26055 RVA: 0x00190338 File Offset: 0x0018E538
		// (set) Token: 0x060065C8 RID: 26056 RVA: 0x00190340 File Offset: 0x0018E540
		internal string HyperLinkURL
		{
			get
			{
				return this.m_hyperLinkURL;
			}
			set
			{
				this.m_hyperLinkURL = value;
			}
		}

		// Token: 0x17002407 RID: 9223
		// (get) Token: 0x060065C9 RID: 26057 RVA: 0x00190349 File Offset: 0x0018E549
		// (set) Token: 0x060065CA RID: 26058 RVA: 0x00190351 File Offset: 0x0018E551
		internal string BookmarkLink
		{
			get
			{
				return this.m_bookmarkLink;
			}
			set
			{
				this.m_bookmarkLink = value;
			}
		}

		// Token: 0x17002408 RID: 9224
		// (get) Token: 0x060065CB RID: 26059 RVA: 0x0019035A File Offset: 0x0018E55A
		// (set) Token: 0x060065CC RID: 26060 RVA: 0x00190362 File Offset: 0x0018E562
		internal string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x17002409 RID: 9225
		// (get) Token: 0x060065CD RID: 26061 RVA: 0x0019036B File Offset: 0x0018E56B
		// (set) Token: 0x060065CE RID: 26062 RVA: 0x00190373 File Offset: 0x0018E573
		internal string DrillthroughReportName
		{
			get
			{
				return this.m_drillthroughReportName;
			}
			set
			{
				this.m_drillthroughReportName = value;
			}
		}

		// Token: 0x1700240A RID: 9226
		// (get) Token: 0x060065CF RID: 26063 RVA: 0x0019037C File Offset: 0x0018E57C
		// (set) Token: 0x060065D0 RID: 26064 RVA: 0x00190384 File Offset: 0x0018E584
		internal object[] DrillthroughParametersValues
		{
			get
			{
				return this.m_drillthroughParametersValues;
			}
			set
			{
				this.m_drillthroughParametersValues = value;
			}
		}

		// Token: 0x1700240B RID: 9227
		// (get) Token: 0x060065D1 RID: 26065 RVA: 0x0019038D File Offset: 0x0018E58D
		// (set) Token: 0x060065D2 RID: 26066 RVA: 0x00190395 File Offset: 0x0018E595
		internal BoolList DrillthroughParametersOmits
		{
			get
			{
				return this.m_drillthroughParametersOmits;
			}
			set
			{
				this.m_drillthroughParametersOmits = value;
			}
		}

		// Token: 0x1700240C RID: 9228
		// (get) Token: 0x060065D3 RID: 26067 RVA: 0x0019039E File Offset: 0x0018E59E
		// (set) Token: 0x060065D4 RID: 26068 RVA: 0x001903A6 File Offset: 0x0018E5A6
		internal IntList DataSetTokenIDs
		{
			get
			{
				return this.m_dataSetTokenIDs;
			}
			set
			{
				this.m_dataSetTokenIDs = value;
			}
		}

		// Token: 0x060065D5 RID: 26069 RVA: 0x001903B0 File Offset: 0x0018E5B0
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.HyperLinkURL, Token.String),
				new MemberInfo(MemberName.BookmarkLink, Token.String),
				new MemberInfo(MemberName.DrillthroughReportName, Token.String),
				new MemberInfo(MemberName.DrillthroughParameters, Token.Array, ObjectType.Variant),
				new MemberInfo(MemberName.DrillthroughParametersOmits, ObjectType.BoolList),
				new MemberInfo(MemberName.Label, Token.String)
			});
		}

		// Token: 0x040032D2 RID: 13010
		private string m_hyperLinkURL;

		// Token: 0x040032D3 RID: 13011
		private string m_bookmarkLink;

		// Token: 0x040032D4 RID: 13012
		private string m_label;

		// Token: 0x040032D5 RID: 13013
		private string m_drillthroughReportName;

		// Token: 0x040032D6 RID: 13014
		private object[] m_drillthroughParametersValues;

		// Token: 0x040032D7 RID: 13015
		private BoolList m_drillthroughParametersOmits;

		// Token: 0x040032D8 RID: 13016
		[NonSerialized]
		private IntList m_dataSetTokenIDs;
	}
}
