using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200042B RID: 1067
	public abstract class XmlString : IXmlSerializable, ICloneable
	{
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002207 RID: 8711
		protected internal abstract Hashtable ValuesHash { get; }

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x00081C0D File Offset: 0x0007FE0D
		protected internal virtual ICollection StandardValues
		{
			get
			{
				return this.ValuesHash.Keys;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x00081C1A File Offset: 0x0007FE1A
		protected internal virtual string DefaultValue
		{
			get
			{
				return "";
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x00005C88 File Offset: 0x00003E88
		protected internal virtual string[] SortedDisplayStrings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x00005BEF File Offset: 0x00003DEF
		protected internal virtual bool AllowExpressions
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x00081C21 File Offset: 0x0007FE21
		// (set) Token: 0x0600220D RID: 8717 RVA: 0x00081C2C File Offset: 0x0007FE2C
		internal virtual string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(SRErrors.InvalidValue(value));
				}
				if ((!this.AllowExpressions || value == "" || value[0] != '=') && !this.ValuesHash.ContainsKey(value))
				{
					throw new ArgumentException(SRErrors.InvalidValue(value));
				}
				this.m_value = value;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x00081C21 File Offset: 0x0007FE21
		// (set) Token: 0x0600220F RID: 8719 RVA: 0x00081C89 File Offset: 0x0007FE89
		protected internal string RawValue
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x00081C92 File Offset: 0x0007FE92
		// (set) Token: 0x06002211 RID: 8721 RVA: 0x00081CC4 File Offset: 0x0007FEC4
		internal string String
		{
			get
			{
				if (this.ValuesHash.ContainsKey(this.m_value))
				{
					return (string)this.ValuesHash[this.m_value];
				}
				return this.m_value;
			}
			set
			{
				if (value == null || value == "")
				{
					this.RawValue = this.DefaultValue;
					return;
				}
				if (this.AllowExpressions && value[0] == '=')
				{
					this.RawValue = value;
					return;
				}
				IDictionaryEnumerator enumerator = this.ValuesHash.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if ((string)enumerator.Value == value)
					{
						this.RawValue = (string)enumerator.Key;
						return;
					}
				}
				enumerator.Reset();
				while (enumerator.MoveNext())
				{
					if (string.Compare((string)enumerator.Value, value, StringComparison.OrdinalIgnoreCase) == 0)
					{
						this.RawValue = (string)enumerator.Key;
						return;
					}
				}
				throw new ArgumentException(SRErrors.InvalidValue(value));
			}
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x00081D85 File Offset: 0x0007FF85
		public override string ToString()
		{
			return this.String;
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x00081D8D File Offset: 0x0007FF8D
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == base.GetType() && this.Value == ((XmlString)obj).Value;
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x00081DBD File Offset: 0x0007FFBD
		public override int GetHashCode()
		{
			if (this.m_value == null)
			{
				return 0;
			}
			return this.m_value.GetHashCode();
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x00081DD4 File Offset: 0x0007FFD4
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x00005C88 File Offset: 0x00003E88
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x00081DDC File Offset: 0x0007FFDC
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string text = reader.ReadString();
			this.Value = text;
			reader.Skip();
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00081DFD File Offset: 0x0007FFFD
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteString(this.Value);
		}

		// Token: 0x04000EEB RID: 3819
		private string m_value;

		// Token: 0x0200052A RID: 1322
		public abstract class XmlStringListConverter : TypeConverter
		{
			// Token: 0x0600252D RID: 9517
			protected abstract XmlString CreateObject(string value);

			// Token: 0x0600252E RID: 9518 RVA: 0x00087C10 File Offset: 0x00085E10
			public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				ICollection standardValues = this.CreateObject(null).StandardValues;
				return this.GetStandardValues(standardValues);
			}

			// Token: 0x0600252F RID: 9519 RVA: 0x00087C34 File Offset: 0x00085E34
			protected TypeConverter.StandardValuesCollection GetStandardValues(ICollection standardValues)
			{
				XmlString[] array = new XmlString[standardValues.Count];
				int num = 0;
				foreach (object obj in standardValues)
				{
					string text = (string)obj;
					array[num++] = this.CreateObject(text);
				}
				Array.Sort(array, 0, array.Length, new XmlString.XmlStringComparer());
				return new TypeConverter.StandardValuesCollection(array);
			}

			// Token: 0x06002530 RID: 9520 RVA: 0x00087CB8 File Offset: 0x00085EB8
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				return !this.CreateObject(null).AllowExpressions;
			}

			// Token: 0x06002531 RID: 9521 RVA: 0x000053DC File Offset: 0x000035DC
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			// Token: 0x06002532 RID: 9522 RVA: 0x0007FD42 File Offset: 0x0007DF42
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06002533 RID: 9523 RVA: 0x00087CC9 File Offset: 0x00085EC9
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				if (value is string)
				{
					XmlString xmlString = this.CreateObject(null);
					xmlString.String = (string)value;
					return xmlString;
				}
				return base.ConvertFrom(context, culture, value);
			}
		}

		// Token: 0x0200052B RID: 1323
		private sealed class XmlStringComparer : IComparer
		{
			// Token: 0x06002535 RID: 9525 RVA: 0x00087CF0 File Offset: 0x00085EF0
			public int Compare(object item1, object item2)
			{
				if (item1 == null)
				{
					if (item2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (item2 == null)
					{
						return 1;
					}
					XmlString xmlString = (XmlString)item1;
					XmlString xmlString2 = (XmlString)item2;
					string[] sortedDisplayStrings = xmlString.SortedDisplayStrings;
					string text = (string)xmlString.ValuesHash[xmlString.Value];
					string text2 = (string)xmlString.ValuesHash[xmlString2.Value];
					int num3;
					if (sortedDisplayStrings != null)
					{
						int num = Array.IndexOf<string>(sortedDisplayStrings, text);
						int num2 = Array.IndexOf<string>(sortedDisplayStrings, text2);
						num3 = ((num < num2) ? (-1) : ((num > num2) ? 1 : 0));
					}
					else if (xmlString.Value == "")
					{
						num3 = ((xmlString2.Value != "") ? (-1) : 0);
					}
					else if (xmlString2.Value == "")
					{
						num3 = 1;
					}
					else
					{
						num3 = string.CompareOrdinal(text, text2);
					}
					return num3;
				}
			}
		}
	}
}
