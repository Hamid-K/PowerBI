using System;
using System.Data.Common;
using System.Diagnostics;
using System.Security.Permissions;
using System.Xml;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Teradata;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.Teradata
{
	// Token: 0x0200001B RID: 27
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class TdSqlSQCommand : SqlSQCommand
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00006CB4 File Offset: 0x00004EB4
		public override void SetConfiguration(string configuration)
		{
			base.SetConfiguration(configuration);
			if (!string.IsNullOrEmpty(configuration))
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml("<Config>" + configuration + "</Config>");
					XmlElement xmlElement = xmlDocument.SelectSingleNode("//ReplaceFunctionName") as XmlElement;
					if (xmlElement != null)
					{
						this.m_replaceFunctionName = xmlElement.InnerText;
					}
				}
				catch (Exception ex)
				{
					throw new SemanticQueryEngineException(SR.InvalidConfiguration(this.LocalizedName, ex.Message));
				}
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006D34 File Offset: 0x00004F34
		public override string LocalizedName
		{
			get
			{
				return "SMQL translator to Teradata";
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006D3B File Offset: 0x00004F3B
		internal override SqlBatch CreateSqlBatch(SemanticModel model, DbConnection dbConnection)
		{
			if (dbConnection == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("dbConnection"));
			}
			return new TdSqlBatch(model, dbConnection.ServerVersion, this.ReplaceFunctionName, base.EnableMathOpCasting);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006D68 File Offset: 0x00004F68
		internal override SqlSQDataReader CreateSqlSQDataReader(CompiledSql compiledSql, bool? dataIsCaseSensitive, IDataReader targetDataReader, ITraceLog traceLog)
		{
			return new TdSqlSQDataReader(compiledSql, dataIsCaseSensitive, targetDataReader, traceLog);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006D74 File Offset: 0x00004F74
		internal string ReplaceFunctionName
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_replaceFunctionName;
			}
		}

		// Token: 0x0400006A RID: 106
		private string m_replaceFunctionName = "oREPLACE";
	}
}
