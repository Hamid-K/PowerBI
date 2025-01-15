using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.HostIntegration.StaticSqlUtil
{
	// Token: 0x02000A79 RID: 2681
	public class Parameter
	{
		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x0600533E RID: 21310 RVA: 0x001528F6 File Offset: 0x00150AF6
		// (set) Token: 0x0600533F RID: 21311 RVA: 0x001528FE File Offset: 0x00150AFE
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x06005340 RID: 21312 RVA: 0x00152907 File Offset: 0x00150B07
		// (set) Token: 0x06005341 RID: 21313 RVA: 0x0015290F File Offset: 0x00150B0F
		public ParameterTypes Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x06005342 RID: 21314 RVA: 0x00152918 File Offset: 0x00150B18
		// (set) Token: 0x06005343 RID: 21315 RVA: 0x00152920 File Offset: 0x00150B20
		public short Ccsid
		{
			get
			{
				return this._ccsid;
			}
			set
			{
				this._ccsid = value;
			}
		}

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06005344 RID: 21316 RVA: 0x00152929 File Offset: 0x00150B29
		// (set) Token: 0x06005345 RID: 21317 RVA: 0x00152931 File Offset: 0x00150B31
		public short Length
		{
			get
			{
				return this._length;
			}
			set
			{
				this._length = value;
			}
		}

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x06005346 RID: 21318 RVA: 0x0015293A File Offset: 0x00150B3A
		// (set) Token: 0x06005347 RID: 21319 RVA: 0x00152942 File Offset: 0x00150B42
		public short Scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				this._scale = value;
			}
		}

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x06005348 RID: 21320 RVA: 0x0015294B File Offset: 0x00150B4B
		// (set) Token: 0x06005349 RID: 21321 RVA: 0x00152953 File Offset: 0x00150B53
		public short Precision
		{
			get
			{
				return this._precision;
			}
			set
			{
				this._precision = value;
			}
		}

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x0600534A RID: 21322 RVA: 0x0015295C File Offset: 0x00150B5C
		// (set) Token: 0x0600534B RID: 21323 RVA: 0x00152964 File Offset: 0x00150B64
		public bool Nullable
		{
			get
			{
				return this._nullable;
			}
			set
			{
				this._nullable = value;
			}
		}

		// Token: 0x0600534C RID: 21324 RVA: 0x00152970 File Offset: 0x00150B70
		static Parameter()
		{
			foreach (object obj in Enum.GetValues(typeof(ParameterTypes)))
			{
				ParameterTypes parameterTypes = (ParameterTypes)obj;
				Parameter._typeMapping.Add(Enum.GetName(typeof(ParameterTypes), parameterTypes).ToLowerInvariant(), parameterTypes);
			}
			Parameter._typeMapping.Add("integer", ParameterTypes.Int);
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x00152A0C File Offset: 0x00150C0C
		internal void SaveToXml(XmlWriter writer)
		{
			writer.WriteStartElement(this._localName);
			writer.WriteAttributeString("name", this._name);
			writer.WriteAttributeString("nullable", this._nullable.ToString());
			writer.WriteStartElement(Enum.GetName(typeof(ParameterTypes), this._type));
			writer.WriteAttributeString("length", this._length.ToString());
			if (this._type == ParameterTypes.Decimal || this._type == ParameterTypes.Numeric)
			{
				writer.WriteAttributeString("precision", this._precision.ToString());
				writer.WriteAttributeString("scale", this._scale.ToString());
			}
			if (this._type == ParameterTypes.Char || this._type == ParameterTypes.VarChar || this._type == ParameterTypes.Graphic || this._type == ParameterTypes.VarGraphic)
			{
				writer.WriteAttributeString("ccsid", this._ccsid.ToString());
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x00152B08 File Offset: 0x00150D08
		internal void LoadFromXml(XmlElement paramElement, XmlNamespaceManager nsmgr)
		{
			if (paramElement.Attributes["nullable"] != null)
			{
				bool.TryParse(paramElement.Attributes["nullable"].Value, out this._nullable);
			}
			if (paramElement.Attributes["name"] != null)
			{
				this._name = paramElement.Attributes["name"].Value;
			}
			foreach (object obj in paramElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				Parameter._typeMapping.TryGetValue(xmlNode.Name.ToLowerInvariant(), out this._type);
				if (xmlNode.Attributes["length"] != null)
				{
					short.TryParse(xmlNode.Attributes["length"].Value, out this._length);
				}
				if (xmlNode.Attributes["precision"] != null)
				{
					short.TryParse(xmlNode.Attributes["precision"].Value, out this._precision);
				}
				if (xmlNode.Attributes["scale"] != null)
				{
					short.TryParse(xmlNode.Attributes["scale"].Value, out this._scale);
				}
				if (xmlNode.Attributes["ccsid"] != null)
				{
					short.TryParse(xmlNode.Attributes["ccsid"].Value, out this._ccsid);
				}
				this.AdjustParameterLength();
			}
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x00152CBC File Offset: 0x00150EBC
		internal void LoadFromXmlV8(XmlElement paramElement)
		{
			if (paramElement.Attributes["Length"] != null)
			{
				short.TryParse(paramElement.Attributes["Length"].Value, out this._length);
			}
			if (paramElement.Attributes["Precision"] != null)
			{
				short.TryParse(paramElement.Attributes["Precision"].Value, out this._precision);
			}
			if (paramElement.Attributes["Scale"] != null)
			{
				short.TryParse(paramElement.Attributes["Scale"].Value, out this._scale);
			}
			if (paramElement.Attributes["Ccsid"] != null)
			{
				short.TryParse(paramElement.Attributes["Ccsid"].Value, out this._ccsid);
			}
			if (paramElement.Attributes["Nullable"] != null)
			{
				bool.TryParse(paramElement.Attributes["Nullable"].Value, out this._nullable);
			}
			if (paramElement.Attributes["Name"] != null)
			{
				this._name = paramElement.Attributes["Name"].Value;
			}
			if (paramElement.Attributes["Type"] != null)
			{
				string value = paramElement.Attributes["Type"].Value;
				Parameter._typeMapping.TryGetValue(value.ToLowerInvariant(), out this._type);
				this.AdjustParameterLength();
			}
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x00152E3C File Offset: 0x0015103C
		private void AdjustParameterLength()
		{
			switch (this._type)
			{
			case ParameterTypes.BigInt:
				this._length = 8;
				return;
			case ParameterTypes.CharForBit:
			case ParameterTypes.Decimal:
			case ParameterTypes.Graphic:
			case ParameterTypes.Numeric:
				break;
			case ParameterTypes.Date:
				this._length = 10;
				return;
			case ParameterTypes.Double:
				this._length = 8;
				return;
			case ParameterTypes.Int:
				this._length = 4;
				return;
			case ParameterTypes.Real:
				this._length = 4;
				return;
			case ParameterTypes.SmallInt:
				this._length = 2;
				return;
			case ParameterTypes.Time:
				this._length = 8;
				return;
			case ParameterTypes.Timestamp:
				this._length = 26;
				break;
			default:
				return;
			}
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x00152ECC File Offset: 0x001510CC
		internal void SaveToXmlV8(XmlWriter writer)
		{
			writer.WriteStartElement(char.ToUpper(this._localName[0]).ToString() + this._localName.Substring(1));
			writer.WriteAttributeString("Name", this._name);
			writer.WriteAttributeString("Type", Enum.GetName(typeof(ParameterTypes), this._type));
			writer.WriteAttributeString("Length", this._length.ToString());
			writer.WriteAttributeString("Precision", this._precision.ToString());
			writer.WriteAttributeString("Scale", this._scale.ToString());
			writer.WriteAttributeString("CCSID", this._ccsid.ToString());
			writer.WriteAttributeString("Nullable", this._nullable.ToString());
			writer.WriteEndElement();
		}

		// Token: 0x04004255 RID: 16981
		private static Dictionary<string, ParameterTypes> _typeMapping = new Dictionary<string, ParameterTypes>();

		// Token: 0x04004256 RID: 16982
		protected string _localName = "parameter";

		// Token: 0x04004257 RID: 16983
		protected string _name;

		// Token: 0x04004258 RID: 16984
		protected ParameterTypes _type;

		// Token: 0x04004259 RID: 16985
		protected short _ccsid;

		// Token: 0x0400425A RID: 16986
		protected short _length;

		// Token: 0x0400425B RID: 16987
		protected short _scale;

		// Token: 0x0400425C RID: 16988
		protected short _precision;

		// Token: 0x0400425D RID: 16989
		protected bool _nullable = true;
	}
}
