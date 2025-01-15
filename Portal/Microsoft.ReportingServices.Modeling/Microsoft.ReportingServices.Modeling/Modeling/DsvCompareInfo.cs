using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000068 RID: 104
	public sealed class DsvCompareInfo : ModelingObject
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x0000E678 File Offset: 0x0000C878
		internal static DsvCompareInfo FromXml(string xml)
		{
			DsvCompareInfo dsvCompareInfo = new DsvCompareInfo();
			dsvCompareInfo.LoadXml(xml);
			return dsvCompareInfo;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000E686 File Offset: 0x0000C886
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0000E68E File Offset: 0x0000C88E
		public CultureInfo Culture
		{
			get
			{
				return this.m_culture;
			}
			set
			{
				base.CheckWriteable();
				this.m_culture = ((value == null) ? null : CultureInfo.ReadOnly(value));
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		public bool IgnoreCase
		{
			get
			{
				return this.m_ignoreCase;
			}
			set
			{
				base.CheckWriteable();
				this.m_ignoreCase = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000E6BF File Offset: 0x0000C8BF
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0000E6C7 File Offset: 0x0000C8C7
		public bool IgnoreKanaType
		{
			get
			{
				return this.m_ignoreKanaType;
			}
			set
			{
				base.CheckWriteable();
				this.m_ignoreKanaType = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000E6D6 File Offset: 0x0000C8D6
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0000E6DE File Offset: 0x0000C8DE
		public bool IgnoreNonSpace
		{
			get
			{
				return this.m_ignoreNonSpace;
			}
			set
			{
				base.CheckWriteable();
				this.m_ignoreNonSpace = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000E6ED File Offset: 0x0000C8ED
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x0000E6F5 File Offset: 0x0000C8F5
		public bool IgnoreWidth
		{
			get
			{
				return this.m_ignoreWidth;
			}
			set
			{
				base.CheckWriteable();
				this.m_ignoreWidth = value;
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000E704 File Offset: 0x0000C904
		public IEqualityComparer<string> CreateComparer()
		{
			if (this.m_culture == null)
			{
				throw new InvalidOperationException();
			}
			return StringUtil.CreateComparer(this.m_culture, this.GetCompareOptions());
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000E728 File Offset: 0x0000C928
		public CompareOptions GetCompareOptions()
		{
			CompareOptions compareOptions = CompareOptions.None;
			if (this.m_ignoreCase)
			{
				compareOptions |= CompareOptions.IgnoreCase;
			}
			if (this.m_ignoreKanaType)
			{
				compareOptions |= CompareOptions.IgnoreKanaType;
			}
			if (this.m_ignoreNonSpace)
			{
				compareOptions |= CompareOptions.IgnoreNonSpace;
			}
			if (this.m_ignoreWidth)
			{
				compareOptions |= CompareOptions.IgnoreWidth;
			}
			return compareOptions;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000E769 File Offset: 0x0000C969
		public override string ToString()
		{
			return XmlFragmentUtil.ToXmlString(new Action<XmlWriter>(this.WriteXml));
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000E77C File Offset: 0x0000C97C
		internal void LoadXml(string xml)
		{
			using (XmlReader xmlReader = XmlFragmentUtil.ReadXmlString(xml))
			{
				XmlUtil.CheckElement(xmlReader, "CompareInfo", string.Empty);
				XmlUtil.LoadDirectChildren(xmlReader, "CompareInfo", string.Empty, new XmlUtil.LoadXmlElementLDC(this.LoadXmlElement));
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000E7D8 File Offset: 0x0000C9D8
		private bool LoadXmlElement(XmlReader xr)
		{
			try
			{
				string localName = xr.LocalName;
				if (!(localName == "Culture"))
				{
					if (!(localName == "IgnoreCase"))
					{
						if (!(localName == "IgnoreKanaType"))
						{
							if (!(localName == "IgnoreNonSpace"))
							{
								if (!(localName == "IgnoreWidth"))
								{
									return false;
								}
								this.m_ignoreWidth = xr.ReadElementContentAsBoolean();
							}
							else
							{
								this.m_ignoreNonSpace = xr.ReadElementContentAsBoolean();
							}
						}
						else
						{
							this.m_ignoreKanaType = xr.ReadElementContentAsBoolean();
						}
					}
					else
					{
						this.m_ignoreCase = xr.ReadElementContentAsBoolean();
					}
				}
				else
				{
					this.m_culture = XmlConvertEx.ToCultureInfo(xr.ReadElementContentAsString());
				}
			}
			catch (ArgumentException)
			{
			}
			catch (FormatException)
			{
			}
			return true;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
		private void WriteXml(XmlWriter xw)
		{
			xw.WriteStartElement("CompareInfo");
			if (this.m_culture != null)
			{
				xw.WriteElementString("Culture", XmlConvertEx.ToString(this.m_culture));
			}
			DsvCompareInfo.WriteElementIfNonDefault<bool>(xw, "IgnoreCase", this.m_ignoreCase);
			DsvCompareInfo.WriteElementIfNonDefault<bool>(xw, "IgnoreKanaType", this.m_ignoreKanaType);
			DsvCompareInfo.WriteElementIfNonDefault<bool>(xw, "IgnoreNonSpace", this.m_ignoreNonSpace);
			DsvCompareInfo.WriteElementIfNonDefault<bool>(xw, "IgnoreWidth", this.m_ignoreWidth);
			xw.WriteEndElement();
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000E928 File Offset: 0x0000CB28
		private static void WriteElementIfNonDefault<T>(XmlWriter xw, string elementName, T value)
		{
			if (value != null && !object.Equals(value, default(T)) && value as string != string.Empty)
			{
				xw.WriteStartElement(elementName);
				xw.WriteValue(value);
				xw.WriteEndElement();
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0000E988 File Offset: 0x0000CB88
		internal void Compile(CompilationContext ctx)
		{
			base.Compile(ctx.ShouldPersist);
		}

		// Token: 0x0400025D RID: 605
		private const string CompareInfoElem = "CompareInfo";

		// Token: 0x0400025E RID: 606
		private const string CultureElem = "Culture";

		// Token: 0x0400025F RID: 607
		private const string IgnoreCaseElem = "IgnoreCase";

		// Token: 0x04000260 RID: 608
		private const string IgnoreKanaTypeElem = "IgnoreKanaType";

		// Token: 0x04000261 RID: 609
		private const string IgnoreNonSpaceElem = "IgnoreNonSpace";

		// Token: 0x04000262 RID: 610
		private const string IgnoreWidthElem = "IgnoreWidth";

		// Token: 0x04000263 RID: 611
		private CultureInfo m_culture;

		// Token: 0x04000264 RID: 612
		private bool m_ignoreCase;

		// Token: 0x04000265 RID: 613
		private bool m_ignoreKanaType;

		// Token: 0x04000266 RID: 614
		private bool m_ignoreNonSpace;

		// Token: 0x04000267 RID: 615
		private bool m_ignoreWidth;
	}
}
