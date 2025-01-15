using System;
using System.Text;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200005E RID: 94
	public sealed class ReportParameter
	{
		// Token: 0x06000696 RID: 1686 RVA: 0x00019442 File Offset: 0x00017642
		internal ReportParameter(ParameterInfo param)
		{
			this.m_underlyingParam = param;
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00019451 File Offset: 0x00017651
		public string Name
		{
			get
			{
				return this.m_underlyingParam.Name;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001945E File Offset: 0x0001765E
		public TypeCode DataType
		{
			get
			{
				return (TypeCode)this.m_underlyingParam.DataType;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001946B File Offset: 0x0001766B
		public bool Nullable
		{
			get
			{
				return this.m_underlyingParam.Nullable;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00019478 File Offset: 0x00017678
		public bool MultiValue
		{
			get
			{
				return this.m_underlyingParam.MultiValue;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00019485 File Offset: 0x00017685
		public bool AllowBlank
		{
			get
			{
				return this.m_underlyingParam.AllowBlank;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00019492 File Offset: 0x00017692
		public string Prompt
		{
			get
			{
				return this.m_underlyingParam.Prompt;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0001949F File Offset: 0x0001769F
		public bool UsedInQuery
		{
			get
			{
				return this.m_underlyingParam.UsedInQuery;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x000194AC File Offset: 0x000176AC
		public object Value
		{
			get
			{
				if (this.m_underlyingParam.Values == null || this.m_underlyingParam.Values.Length == 0)
				{
					return null;
				}
				return this.m_underlyingParam.Values[0];
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x000194D8 File Offset: 0x000176D8
		public object[] Values
		{
			get
			{
				if (this.m_underlyingParam.Values == null || this.m_underlyingParam.Values.Length == 0)
				{
					return null;
				}
				return this.m_underlyingParam.Values;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00019504 File Offset: 0x00017704
		internal string StringValues
		{
			get
			{
				if (this.m_underlyingParam.Values == null || this.m_underlyingParam.Values.Length == 0)
				{
					return null;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < this.m_underlyingParam.Values.Length; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(this.m_underlyingParam.CastToString(this.m_underlyingParam.Values[i], Localization.ClientPrimaryCulture));
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00019585 File Offset: 0x00017785
		internal ParameterInfo UnderlyingParam
		{
			get
			{
				return this.m_underlyingParam;
			}
		}

		// Token: 0x040001B7 RID: 439
		private ParameterInfo m_underlyingParam;
	}
}
