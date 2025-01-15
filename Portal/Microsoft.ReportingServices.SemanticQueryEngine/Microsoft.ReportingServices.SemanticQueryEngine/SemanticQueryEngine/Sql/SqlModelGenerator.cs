using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Interfaces;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.Modeling.ModelGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql
{
	// Token: 0x02000017 RID: 23
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class SqlModelGenerator : ISemanticModelGenerator, IExtension, ITraceableComponent
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00003FB8 File Offset: 0x000021B8
		public void SetConfiguration(string configuration)
		{
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000066E6 File Offset: 0x000048E6
		public string LocalizedName
		{
			get
			{
				return SR.SqlModelGeneratorLocalizedName;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000066F0 File Offset: 0x000048F0
		void ISemanticModelGenerator.Generate(IDbConnection connection, XmlWriter newModelWriter)
		{
			if (connection == null || connection.State != ConnectionState.Open)
			{
				throw SQEAssert.AssertFalseAndThrow("Connection must be open.", Array.Empty<object>());
			}
			if (newModelWriter == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("newModelWriter"));
			}
			SemanticModel semanticModel = new SemanticModel();
			semanticModel.Culture = SemanticQueryConnection.DefaultModelCulture;
			this.FillModel(semanticModel, connection);
			semanticModel.WriteTo(newModelWriter);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000674C File Offset: 0x0000494C
		void ISemanticModelGenerator.ReGenerateModel(IDbConnection connection, XmlReader currentModelReader, XmlWriter newModelWriter)
		{
			if (connection == null || connection.State != ConnectionState.Open)
			{
				throw SQEAssert.AssertFalseAndThrow("Connection must be open.", Array.Empty<object>());
			}
			if (currentModelReader == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("currentModelReader"));
			}
			if (newModelWriter == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("newModelWriter"));
			}
			SemanticModel semanticModel = new SemanticModel();
			semanticModel.Load(currentModelReader);
			SemanticModel semanticModel2 = new SemanticModel();
			semanticModel2.Culture = semanticModel.Culture;
			semanticModel2.DataSourceView = new DataSourceView(semanticModel.DataSourceView.ID, semanticModel.DataSourceView.Name, semanticModel.DataSourceView.DataSourceID);
			this.FillModel(semanticModel2, connection);
			SyncModelItemIDsAlgorithm.SyncModelItemIDs(semanticModel, semanticModel2);
			semanticModel2.WriteTo(newModelWriter);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000067FD File Offset: 0x000049FD
		void ITraceableComponent.SetTraceLog(ITraceLog traceLog)
		{
			this.m_traceLog = traceLog;
		}

		// Token: 0x06000133 RID: 307
		internal abstract SqlDsvGenerator GetSqlDsvGenerator(IDbConnection connection);

		// Token: 0x06000134 RID: 308
		internal abstract SqlDsvStatisticsProvider GetSqlDsvStatisticsProvider(IDbConnection connection);

		// Token: 0x06000135 RID: 309 RVA: 0x00006808 File Offset: 0x00004A08
		private void FillModel(SemanticModel model, IDbConnection connection)
		{
			SqlDsvGenerator sqlDsvGenerator = this.GetSqlDsvGenerator(connection);
			if (this.m_traceLog != null && sqlDsvGenerator != null)
			{
				((ITraceableComponent)sqlDsvGenerator).SetTraceLog(this.m_traceLog);
			}
			MemoryStream memoryStream = new MemoryStream();
			using (XmlWriter xmlWriter = Microsoft.ReportingServices.Common.XmlRWFactory.CreateWriter(memoryStream))
			{
				sqlDsvGenerator.Generate(xmlWriter, model.DataSourceView);
				xmlWriter.Flush();
				memoryStream.Position = 0L;
				using (XmlReader xmlReader = Microsoft.ReportingServices.Common.XmlRWFactory.CreateReader(memoryStream))
				{
					model.DataSourceView = new DataSourceView();
					model.DataSourceView.Load(xmlReader);
				}
			}
			if (this.m_traceLog != null && this.m_traceLog.TraceVerbose)
			{
				this.m_traceLog.WriteTrace("Deriving semantic model from the DSV...", TraceLevel.Verbose);
			}
			ModelGen modelGen = new ModelGen();
			modelGen.Model = model;
			modelGen.DsvStatisticsProvider = this.GetSqlDsvStatisticsProvider(connection);
			modelGen.OverwriteDsvStatistics = true;
			modelGen.TraceVerbose = true;
			modelGen.RuleSet = this.LoadRuleSet();
			modelGen.Log += this.Log;
			modelGen.Run();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006920 File Offset: 0x00004B20
		private void Log(object sender, ModelGenLogEventArgs e)
		{
			if (this.m_traceLog != null)
			{
				string text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("{0} {1} {2} {3}", new object[] { e.EntryType, e.Source, e.DsvItemName, e.Message });
				TraceLevel traceLevel;
				switch (e.EntryType)
				{
				case LogEntryType.Error:
					if (!this.m_traceLog.TraceError)
					{
						return;
					}
					traceLevel = TraceLevel.Error;
					break;
				case LogEntryType.Warning:
					if (!this.m_traceLog.TraceWarning)
					{
						return;
					}
					traceLevel = TraceLevel.Warning;
					break;
				case LogEntryType.Info:
					if (!this.m_traceLog.TraceInfo)
					{
						return;
					}
					traceLevel = TraceLevel.Info;
					break;
				default:
					if (!this.m_traceLog.TraceVerbose)
					{
						return;
					}
					traceLevel = TraceLevel.Verbose;
					break;
				}
				this.m_traceLog.WriteTrace(text, traceLevel);
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000069E4 File Offset: 0x00004BE4
		private RuleSet LoadRuleSet()
		{
			if (Globals.Configuration != null)
			{
				string text = null;
				try
				{
					text = Path.Combine(Globals.Configuration.ConfigFilePath, "ModelGenerationRules.smgl");
					using (XmlReader xmlReader = Microsoft.ReportingServices.Common.XmlRWFactory.CreateReader(File.OpenRead(text)))
					{
						return this.LoadRuleSet(xmlReader);
					}
				}
				catch (Exception ex)
				{
					if (this.m_traceLog != null)
					{
						try
						{
							this.m_traceLog.WriteTrace(Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("Failed to load smgl file, using embedded. Path: {0}. Exception: {1}", new object[] { text, ex }), TraceLevel.Error);
						}
						catch
						{
						}
					}
				}
			}
			RuleSet ruleSet;
			using (XmlReader xmlReader2 = Microsoft.ReportingServices.Common.XmlRWFactory.CreateReader(typeof(SqlModelGenerator).Assembly.GetManifestResourceStream("Microsoft.ReportingServices.SemanticQueryEngine.ModelGenerationRules.smgl")))
			{
				ruleSet = this.LoadRuleSet(xmlReader2);
			}
			return ruleSet;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006AD0 File Offset: 0x00004CD0
		private RuleSet LoadRuleSet(XmlReader xr)
		{
			RuleSet ruleSet = new RuleSet();
			ruleSet.Load(xr, Localization.ClientPrimaryCulture, SemanticQueryConnection.DefaultModelCulture);
			return ruleSet;
		}

		// Token: 0x04000065 RID: 101
		private ITraceLog m_traceLog;
	}
}
