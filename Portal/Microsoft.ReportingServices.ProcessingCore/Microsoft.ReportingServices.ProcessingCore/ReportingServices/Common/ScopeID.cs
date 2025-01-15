using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005C9 RID: 1481
	internal class ScopeID : IXmlSerializable
	{
		// Token: 0x06005376 RID: 21366 RVA: 0x001600AA File Offset: 0x0015E2AA
		internal ScopeID()
		{
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x001600B2 File Offset: 0x0015E2B2
		internal ScopeID(ScopeValue[] scopeValues)
		{
			this.m_scopeValues = scopeValues;
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x001600C1 File Offset: 0x0015E2C1
		internal ScopeID(ScopeID scopeID)
			: this(scopeID.m_scopeValues)
		{
		}

		// Token: 0x17001EF2 RID: 7922
		// (get) Token: 0x06005379 RID: 21369 RVA: 0x001600CF File Offset: 0x0015E2CF
		public int ScopeValueCount
		{
			get
			{
				if (this.m_scopeValues != null)
				{
					return this.m_scopeValues.Length;
				}
				return 0;
			}
		}

		// Token: 0x17001EF3 RID: 7923
		// (get) Token: 0x0600537A RID: 21370 RVA: 0x001600E3 File Offset: 0x0015E2E3
		public IEnumerable<ScopeValue> InstanceID
		{
			get
			{
				int count = this.ScopeValueCount;
				int num;
				for (int cursor = 0; cursor < count; cursor = num + 1)
				{
					ScopeValue scopeValue = this.m_scopeValues[cursor];
					if (scopeValue.IsGroupExprValue)
					{
						yield return scopeValue;
					}
					num = cursor;
				}
				yield break;
			}
		}

		// Token: 0x17001EF4 RID: 7924
		// (get) Token: 0x0600537B RID: 21371 RVA: 0x001600F3 File Offset: 0x0015E2F3
		public IEnumerable<ScopeValue> QueryRestartPosition
		{
			get
			{
				int count = this.ScopeValueCount;
				int num;
				for (int cursor = 0; cursor < count; cursor = num + 1)
				{
					ScopeValue scopeValue = this.m_scopeValues[cursor];
					if (scopeValue.IsSortExprValue)
					{
						yield return scopeValue;
					}
					num = cursor;
				}
				yield break;
			}
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x00160103 File Offset: 0x0015E303
		public ScopeValue GetScopeValue(int index)
		{
			return this.m_scopeValues[index];
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x00160110 File Offset: 0x0015E310
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{ ");
			string text = string.Empty;
			int scopeValueCount = this.ScopeValueCount;
			for (int i = 0; i < scopeValueCount; i++)
			{
				stringBuilder.Append(text);
				stringBuilder.Append(Convert.ToString(this.m_scopeValues[i].Value, CultureInfo.InvariantCulture));
				stringBuilder.Append("[");
				stringBuilder.Append(this.m_scopeValues[i].ScopeType.ToString());
				stringBuilder.Append("]");
				text = ", ";
			}
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x001601C1 File Offset: 0x0015E3C1
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ScopeID);
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x001601CF File Offset: 0x0015E3CF
		internal bool Equals(ScopeID scopeID)
		{
			return this == scopeID || (scopeID != null && this.Equals(scopeID, null));
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x001601E4 File Offset: 0x0015E3E4
		internal bool Equals(ScopeID scopeID, IEqualityComparer<object> comparer)
		{
			if (scopeID == null)
			{
				return false;
			}
			if (this.m_scopeValues == scopeID.m_scopeValues)
			{
				return true;
			}
			int scopeValueCount = this.ScopeValueCount;
			if (scopeValueCount != scopeID.ScopeValueCount)
			{
				return false;
			}
			for (int i = 0; i < scopeValueCount; i++)
			{
				if (!this.m_scopeValues[i].Equals(scopeID.m_scopeValues[i], comparer))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x00160240 File Offset: 0x0015E440
		internal int Compare(ScopeID scopeID, IComparer<object> comparer)
		{
			int scopeValueCount = this.ScopeValueCount;
			if (scopeValueCount == 0)
			{
				if (scopeID == null || scopeID.ScopeValueCount == 0)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (scopeID == null || scopeID.ScopeValueCount == 0)
				{
					return 1;
				}
				if (scopeValueCount != scopeID.ScopeValueCount)
				{
					throw new ArgumentException();
				}
				for (int i = 0; i < scopeValueCount; i++)
				{
					int num = comparer.Compare(this.m_scopeValues[i].Value, scopeID.m_scopeValues[i].Value);
					if (num != 0)
					{
						return num;
					}
				}
				return 0;
			}
		}

		// Token: 0x06005382 RID: 21378 RVA: 0x001602B8 File Offset: 0x0015E4B8
		internal int GetHashCode(IEqualityComparer<object> comparer)
		{
			int scopeValueCount = this.ScopeValueCount;
			int num = scopeValueCount;
			for (int i = 0; i < scopeValueCount; i++)
			{
				num ^= this.m_scopeValues[i].GetHashCode(comparer);
			}
			return num;
		}

		// Token: 0x06005383 RID: 21379 RVA: 0x001602EC File Offset: 0x0015E4EC
		public static bool operator ==(ScopeID scopeID1, ScopeID scopeID2)
		{
			return scopeID1 == scopeID2 || (scopeID1 != null && scopeID1.Equals(scopeID2));
		}

		// Token: 0x06005384 RID: 21380 RVA: 0x00160300 File Offset: 0x0015E500
		public static bool operator !=(ScopeID scopeID1, ScopeID scopeID2)
		{
			return !(scopeID1 == scopeID2);
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x0016030C File Offset: 0x0015E50C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x00160314 File Offset: 0x0015E514
		public virtual XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005387 RID: 21383 RVA: 0x0016031B File Offset: 0x0015E51B
		public virtual void ReadXml(XmlReader xmlReader)
		{
			while (xmlReader.Read())
			{
				if (xmlReader.NodeType == XmlNodeType.Element)
				{
					this.ReadXmlElement(xmlReader);
				}
			}
		}

		// Token: 0x06005388 RID: 21384 RVA: 0x00160338 File Offset: 0x0015E538
		protected virtual void ReadXmlElement(XmlReader xmlReader)
		{
			if (xmlReader.Name != "ScopeValues")
			{
				return;
			}
			List<ScopeValue> list = new List<ScopeValue>();
			using (XmlReader xmlReader2 = xmlReader.ReadSubtree())
			{
				while (xmlReader2.Read())
				{
					if (xmlReader2.NodeType == XmlNodeType.Element && xmlReader2.Name == "ScopeValue")
					{
						using (XmlReader xmlReader3 = xmlReader.ReadSubtree())
						{
							ScopeValue scopeValue = new ScopeValue();
							scopeValue.ReadXml(xmlReader3);
							list.Add(scopeValue);
						}
					}
				}
			}
			if (list.Count > 0)
			{
				this.m_scopeValues = list.ToArray();
				return;
			}
			this.m_scopeValues = null;
		}

		// Token: 0x06005389 RID: 21385 RVA: 0x001603F8 File Offset: 0x0015E5F8
		public virtual void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("ScopeID");
			this.WriteBaseXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x0600538A RID: 21386 RVA: 0x00160414 File Offset: 0x0015E614
		protected void WriteBaseXml(XmlWriter writer)
		{
			writer.WriteStartElement("ScopeValues");
			ScopeValue[] scopeValues = this.m_scopeValues;
			for (int i = 0; i < scopeValues.Length; i++)
			{
				scopeValues[i].WriteXml(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04002A17 RID: 10775
		private ScopeValue[] m_scopeValues;

		// Token: 0x04002A18 RID: 10776
		internal const string SCOPEID = "ScopeID";

		// Token: 0x04002A19 RID: 10777
		internal const string SCOPEVALUES = "ScopeValues";
	}
}
