using System;
using System.Xml;

namespace Microsoft.HostIntegration.StaticSqlUtil
{
	// Token: 0x02000A7C RID: 2684
	public class Statement
	{
		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x06005368 RID: 21352 RVA: 0x001535C8 File Offset: 0x001517C8
		// (set) Token: 0x06005369 RID: 21353 RVA: 0x001535D0 File Offset: 0x001517D0
		public bool SqlStatementAssumptions
		{
			get
			{
				return this._sqlStatementAssumptions;
			}
			set
			{
				this._sqlStatementAssumptions = value;
			}
		}

		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x0600536A RID: 21354 RVA: 0x001535D9 File Offset: 0x001517D9
		// (set) Token: 0x0600536B RID: 21355 RVA: 0x001535E1 File Offset: 0x001517E1
		public int SqlStatementNumber
		{
			get
			{
				return this._sqlStatementNumber;
			}
			set
			{
				this._sqlStatementNumber = value;
			}
		}

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x0600536C RID: 21356 RVA: 0x001535EA File Offset: 0x001517EA
		// (set) Token: 0x0600536D RID: 21357 RVA: 0x001535F2 File Offset: 0x001517F2
		public string SqlStatement
		{
			get
			{
				return this._sqlStatement;
			}
			set
			{
				this._sqlStatement = value;
			}
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x001535FC File Offset: 0x001517FC
		internal void SaveToXml(XmlWriter writer)
		{
			writer.WriteStartElement("statement");
			writer.WriteAttributeString("sqlStatementNumber", this._sqlStatementNumber.ToString());
			writer.WriteAttributeString("sqlStatementAssumptions", this._sqlStatementAssumptions.ToString());
			writer.WriteAttributeString("sqlStatement", this._sqlStatement);
			writer.WriteEndElement();
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x00153658 File Offset: 0x00151858
		internal void LoadFromXml(XmlElement stmtElement, XmlNamespaceManager nsmgr)
		{
			if (stmtElement.Attributes["sqlStatementNumber"] != null)
			{
				int.TryParse(stmtElement.Attributes["sqlStatementNumber"].Value, out this._sqlStatementNumber);
			}
			if (stmtElement.Attributes["sqlStatementAssumptions"] != null)
			{
				this._sqlStatementAssumptions = bool.Parse(stmtElement.Attributes["sqlStatementAssumptions"].Value);
			}
			if (stmtElement.Attributes["sqlStatement"] != null)
			{
				this._sqlStatement = stmtElement.Attributes["sqlStatement"].Value;
			}
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x001536F8 File Offset: 0x001518F8
		internal void LoadFromXmlV8(XmlElement stmtElement)
		{
			if (stmtElement.Attributes["Number"] != null)
			{
				int.TryParse(stmtElement.Attributes["Number"].Value, out this._sqlStatementNumber);
			}
			this._sqlStatement = stmtElement.InnerText;
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x00153744 File Offset: 0x00151944
		internal void SaveToXmlV8(XmlWriter writer)
		{
			writer.WriteStartElement("Statement");
			writer.WriteAttributeString("Number", this._sqlStatementNumber.ToString());
			writer.WriteValue(this._sqlStatement);
			writer.WriteEndElement();
		}

		// Token: 0x04004264 RID: 16996
		private int _sqlStatementNumber = -1;

		// Token: 0x04004265 RID: 16997
		private string _sqlStatement;

		// Token: 0x04004266 RID: 16998
		private bool _sqlStatementAssumptions = true;
	}
}
