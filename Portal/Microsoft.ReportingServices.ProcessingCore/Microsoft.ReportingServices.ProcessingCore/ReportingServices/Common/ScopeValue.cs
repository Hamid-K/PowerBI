using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005C8 RID: 1480
	internal sealed class ScopeValue : SerializableValue
	{
		// Token: 0x06005364 RID: 21348 RVA: 0x0015FE8C File Offset: 0x0015E08C
		internal ScopeValue()
		{
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x0015FE9B File Offset: 0x0015E09B
		internal ScopeValue(object value, ScopeIDType scopeType)
			: base(value)
		{
			this.m_scopeType = scopeType;
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x0015FEB2 File Offset: 0x0015E0B2
		internal ScopeValue(object value, ScopeIDType scopeType, string key)
			: this(value, scopeType)
		{
			this.m_key = key;
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x0015FEC3 File Offset: 0x0015E0C3
		internal ScopeValue(object value, ScopeIDType scopeType, DataTypeCode dataTypeCode)
			: base(value, dataTypeCode)
		{
			this.m_scopeType = scopeType;
		}

		// Token: 0x17001EEE RID: 7918
		// (get) Token: 0x06005368 RID: 21352 RVA: 0x0015FEDB File Offset: 0x0015E0DB
		internal ScopeIDType ScopeType
		{
			get
			{
				return this.m_scopeType;
			}
		}

		// Token: 0x17001EEF RID: 7919
		// (get) Token: 0x06005369 RID: 21353 RVA: 0x0015FEE3 File Offset: 0x0015E0E3
		internal string Key
		{
			get
			{
				return this.m_key;
			}
		}

		// Token: 0x17001EF0 RID: 7920
		// (get) Token: 0x0600536A RID: 21354 RVA: 0x0015FEEB File Offset: 0x0015E0EB
		internal bool IsGroupExprValue
		{
			get
			{
				return this.m_scopeType == ScopeIDType.GroupValues || this.m_scopeType == ScopeIDType.SortGroup;
			}
		}

		// Token: 0x17001EF1 RID: 7921
		// (get) Token: 0x0600536B RID: 21355 RVA: 0x0015FF01 File Offset: 0x0015E101
		internal bool IsSortExprValue
		{
			get
			{
				return this.m_scopeType == ScopeIDType.SortValues || this.m_scopeType == ScopeIDType.SortGroup;
			}
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x0015FF17 File Offset: 0x0015E117
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ScopeValue);
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x0015FF25 File Offset: 0x0015E125
		internal bool Equals(ScopeValue scopeValue)
		{
			return this == scopeValue || (scopeValue != null && this.Equals(scopeValue, null));
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x0015FF3A File Offset: 0x0015E13A
		public static bool operator ==(ScopeValue scopeValue1, ScopeValue scopeValue2)
		{
			return scopeValue1 == scopeValue2 || (scopeValue1 != null && scopeValue1.Equals(scopeValue2));
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x0015FF4E File Offset: 0x0015E14E
		public static bool operator !=(ScopeValue scopeValue1, ScopeValue scopeValue2)
		{
			return !(scopeValue1 == scopeValue2);
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x0015FF5C File Offset: 0x0015E15C
		internal bool Equals(ScopeValue scopeValue, IEqualityComparer<object> comparer)
		{
			if (scopeValue == null)
			{
				return false;
			}
			if (this.ScopeType != scopeValue.ScopeType)
			{
				return false;
			}
			if (comparer == null)
			{
				return ObjectSerializer.Equals(base.Value, scopeValue.Value, base.TypeCode, scopeValue.TypeCode);
			}
			return comparer.Equals(base.Value, scopeValue.Value);
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x0015FFB4 File Offset: 0x0015E1B4
		internal int GetHashCode(IEqualityComparer<object> comparer)
		{
			int num = 0;
			if (base.Value != null)
			{
				num = comparer.GetHashCode(base.Value);
				num <<= 20;
			}
			return num | (int)(((int)this.ScopeType << 8) & (ScopeIDType)base.TypeCode);
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x0015FFF0 File Offset: 0x0015E1F0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x0015FFF8 File Offset: 0x0015E1F8
		protected override void ReadDerivedXml(XmlReader xmlReader)
		{
			if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "ScopeValue")
			{
				this.ReadAttributes(xmlReader);
			}
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x0016001C File Offset: 0x0015E21C
		private void ReadAttributes(XmlReader xmlReader)
		{
			for (int i = 0; i < xmlReader.AttributeCount; i++)
			{
				xmlReader.MoveToAttribute(i);
				if (xmlReader.Name == "ScopeType")
				{
					this.m_scopeType = (ScopeIDType)Enum.Parse(typeof(ScopeIDType), xmlReader.Value, false);
				}
			}
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x00160074 File Offset: 0x0015E274
		public override void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("ScopeValue");
			writer.WriteAttributeString("ScopeType", this.m_scopeType.ToString());
			base.WriteBaseXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x04002A13 RID: 10771
		private readonly string m_key;

		// Token: 0x04002A14 RID: 10772
		private ScopeIDType m_scopeType = ScopeIDType.GroupValues;

		// Token: 0x04002A15 RID: 10773
		internal const string SCOPEVALUE = "ScopeValue";

		// Token: 0x04002A16 RID: 10774
		internal const string SCOPETYPE = "ScopeType";
	}
}
