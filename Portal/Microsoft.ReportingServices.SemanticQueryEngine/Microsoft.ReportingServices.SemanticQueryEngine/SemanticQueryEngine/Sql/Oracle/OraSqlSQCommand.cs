using System;
using System.Data.Common;
using System.Security.Permissions;
using System.Xml;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Oracle;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.Oracle
{
	// Token: 0x0200001E RID: 30
	public sealed class OraSqlSQCommand : SqlSQCommand
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00006E6C File Offset: 0x0000506C
		[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
		public override void SetConfiguration(string configuration)
		{
			base.SetConfiguration(configuration);
			if (!string.IsNullOrEmpty(configuration))
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml("<Config>" + configuration + "</Config>");
					XmlElement xmlElement = xmlDocument.SelectSingleNode("//DisableNO_MERGEInLeftOuters") as XmlElement;
					if (xmlElement != null)
					{
						this.m_enableNO_MERGEInLeftOuters = !bool.Parse(xmlElement.InnerText);
					}
					xmlElement = xmlDocument.SelectSingleNode("//EnableUnistr") as XmlElement;
					if (xmlElement != null)
					{
						this.m_enableUnistr = bool.Parse(xmlElement.InnerText);
					}
					xmlElement = xmlDocument.SelectSingleNode("//DisableTSTruncation") as XmlElement;
					if (xmlElement != null)
					{
						this.m_enableTSTruncation = !bool.Parse(xmlElement.InnerText);
					}
				}
				catch (Exception ex)
				{
					throw new SemanticQueryEngineException(SR.InvalidConfiguration(this.LocalizedName, ex.Message));
				}
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00006F44 File Offset: 0x00005144
		public override string LocalizedName
		{
			[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
			get
			{
				return "SMQL translator to Oracle";
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006F4B File Offset: 0x0000514B
		internal override SqlBatch CreateSqlBatch(SemanticModel model, DbConnection dbConnection)
		{
			if (dbConnection == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("dbConnection"));
			}
			return new OraSqlBatch(model, dbConnection.ServerVersion, base.EnableMathOpCasting, this.m_enableNO_MERGEInLeftOuters, this.m_enableUnistr, this.m_enableTSTruncation);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006F84 File Offset: 0x00005184
		internal override SqlSQDataReader CreateSqlSQDataReader(CompiledSql compiledSql, bool? dataIsCaseSensitive, IDataReader targetDataReader, ITraceLog traceLog)
		{
			return new OraSqlSQDataReader(compiledSql, dataIsCaseSensitive, targetDataReader, traceLog);
		}

		// Token: 0x0400006B RID: 107
		private bool m_enableNO_MERGEInLeftOuters = true;

		// Token: 0x0400006C RID: 108
		private bool m_enableUnistr;

		// Token: 0x0400006D RID: 109
		private bool m_enableTSTruncation = true;
	}
}
