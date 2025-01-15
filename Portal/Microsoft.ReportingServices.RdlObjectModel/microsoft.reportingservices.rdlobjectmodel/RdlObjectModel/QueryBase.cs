using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000BF RID: 191
	public class QueryBase : ReportObject
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0001BEC3 File Offset: 0x0001A0C3
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0001BED1 File Offset: 0x0001A0D1
		[DefaultValue(CommandTypes.Text)]
		public CommandTypes CommandType
		{
			get
			{
				return (CommandTypes)base.PropertyStore.GetInteger(0);
			}
			set
			{
				base.PropertyStore.SetInteger(0, (int)value);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0001BEE0 File Offset: 0x0001A0E0
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0001BEEE File Offset: 0x0001A0EE
		public ReportExpression CommandText
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0001BF02 File Offset: 0x0001A102
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x0001BF0A File Offset: 0x0001A10A
		[XmlElement(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		public string PreviewCommandText { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0001BF13 File Offset: 0x0001A113
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x0001BF21 File Offset: 0x0001A121
		[DefaultValue(0)]
		[ValidValues(0, 2147483647)]
		public int Timeout
		{
			get
			{
				return base.PropertyStore.GetInteger(2);
			}
			set
			{
				((IntProperty)DefinitionStore<Query, QueryBase.Definition.Properties>.GetProperty(2)).Validate(this, value);
				base.PropertyStore.SetInteger(2, value);
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001BF42 File Offset: 0x0001A142
		public QueryBase()
		{
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001BF4A File Offset: 0x0001A14A
		internal QueryBase(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001BF53 File Offset: 0x0001A153
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001BF5B File Offset: 0x0001A15B
		public bool Equals(QueryBase queryBase)
		{
			return queryBase != null && (this.CommandTextEquivalent(this.CommandText, queryBase.CommandText) && this.CommandType == queryBase.CommandType) && this.Timeout == queryBase.Timeout;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001BF94 File Offset: 0x0001A194
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryBase);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001BFA4 File Offset: 0x0001A1A4
		private bool CommandTextEquivalent(ReportExpression first, ReportExpression second)
		{
			string text = this.FixCommandText(first.ToString());
			string text2 = this.FixCommandText(second.ToString());
			return text == text2;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001BFDE File Offset: 0x0001A1DE
		private string FixCommandText(string text)
		{
			return Regex.Replace(Regex.Replace(text, "(\\r|\\n)", ""), "^\\s*(.*?)\\s*$", "$1");
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001C000 File Offset: 0x0001A200
		public override int GetHashCode()
		{
			return this.CommandText.GetHashCode();
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0001C021 File Offset: 0x0001A221
		// (set) Token: 0x06000806 RID: 2054 RVA: 0x0001C024 File Offset: 0x0001A224
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ExternalXmlElement RQDDesignerState
		{
			get
			{
				return null;
			}
			set
			{
				this.DesignerState = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x0001C02D File Offset: 0x0001A22D
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x0001C035 File Offset: 0x0001A235
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public ExternalXmlElement DesignerState
		{
			get
			{
				return this.m_designerState;
			}
			set
			{
				if (this.m_designerState != value)
				{
					this.m_designerState = value;
				}
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x0001C047 File Offset: 0x0001A247
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x0001C04A File Offset: 0x0001A24A
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public ExternalXmlElement MdxQuery
		{
			get
			{
				return null;
			}
			set
			{
				this.DesignerState = value;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0001C053 File Offset: 0x0001A253
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x0001C05B File Offset: 0x0001A25B
		[DefaultValue(false)]
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public bool AutoGenerated
		{
			get
			{
				return this.m_autoGenerated;
			}
			set
			{
				if (this.m_autoGenerated != value)
				{
					this.m_autoGenerated = value;
				}
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0001C06D File Offset: 0x0001A26D
		// (set) Token: 0x0600080E RID: 2062 RVA: 0x0001C075 File Offset: 0x0001A275
		[DefaultValue(false)]
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public bool Hidden
		{
			get
			{
				return this.m_hidden;
			}
			set
			{
				if (this.m_hidden != value)
				{
					this.m_hidden = value;
				}
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x0001C087 File Offset: 0x0001A287
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x0001C08F File Offset: 0x0001A28F
		[DefaultValue(false)]
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public bool SuppressAutoUpdate
		{
			get
			{
				return this.m_suppressAutoUpdate;
			}
			set
			{
				if (this.m_suppressAutoUpdate != value)
				{
					this.m_suppressAutoUpdate = value;
				}
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0001C0A1 File Offset: 0x0001A2A1
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x0001C0A9 File Offset: 0x0001A2A9
		[DefaultValue(false)]
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public bool UseGenericDesigner
		{
			get
			{
				return this.m_useGenericDesigner;
			}
			set
			{
				if (this.m_useGenericDesigner != value)
				{
					this.m_useGenericDesigner = value;
				}
			}
		}

		// Token: 0x0400015B RID: 347
		protected ExternalXmlElement m_designerState;

		// Token: 0x0400015C RID: 348
		protected bool m_autoGenerated;

		// Token: 0x0400015D RID: 349
		protected bool m_hidden;

		// Token: 0x0400015E RID: 350
		protected bool m_suppressAutoUpdate;

		// Token: 0x0400015F RID: 351
		protected bool m_useGenericDesigner;

		// Token: 0x0200036B RID: 875
		internal class Definition : DefinitionStore<Query, QueryBase.Definition.Properties>
		{
			// Token: 0x02000488 RID: 1160
			internal enum Properties
			{
				// Token: 0x04000B1C RID: 2844
				CommandType,
				// Token: 0x04000B1D RID: 2845
				CommandText,
				// Token: 0x04000B1E RID: 2846
				Timeout
			}
		}
	}
}
