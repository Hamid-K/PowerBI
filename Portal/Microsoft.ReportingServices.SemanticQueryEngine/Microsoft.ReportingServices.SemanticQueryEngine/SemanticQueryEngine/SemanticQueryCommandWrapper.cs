using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine
{
	// Token: 0x02000010 RID: 16
	public sealed class SemanticQueryCommandWrapper : IDbCommand, IDisposable, IDbCommandAnalysis, IDbCommandRewriter
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00004C28 File Offset: 0x00002E28
		internal SemanticQueryCommandWrapper(SemanticModel userModel, SemanticModel fullModel, ISemanticQueryCommand targetCommand, string userID, string userCulture)
		{
			if (userModel == null)
			{
				throw new ArgumentNullException("userModel");
			}
			if (fullModel == null)
			{
				throw new ArgumentNullException("model");
			}
			if (targetCommand == null)
			{
				throw new ArgumentNullException("targetCommand");
			}
			this.m_userModel = userModel;
			this.m_fullModel = fullModel;
			this.m_targetCommand = targetCommand;
			this.m_userID = userID;
			this.m_userCulture = userCulture;
			this.m_targetCommand.CommandType = this.CommandType;
			string text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("Semantic Query Command ID: {0}. Target command type = {1}\r\n{2}", new object[]
			{
				++SemanticQueryCommandWrapper.m_commandIDCounter,
				targetCommand.GetType().Name,
				"{0}"
			});
			this.m_traceLog = new TraceableComponentLog(RSTrace.SQETracer, text);
			ITraceableComponent traceableComponent = this.m_targetCommand as ITraceableComponent;
			if (traceableComponent != null)
			{
				try
				{
					traceableComponent.SetTraceLog(this.m_traceLog);
				}
				catch (Exception ex)
				{
					this.m_traceLog.WriteTrace("Failed to call set trace log on ISemanticQueryCommand object: " + ex.ToString(), TraceLevel.Error);
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004D4C File Offset: 0x00002F4C
		public IDataReader ExecuteReader(CommandBehavior behavior)
		{
			IDataReader dataReader;
			try
			{
				if (behavior == CommandBehavior.SchemaOnly)
				{
					dataReader = this.CreateSchemaOnlyDataReader(this.PrepareQuery(false, false, "ExecuteReader(SchemaOnly)"));
				}
				else
				{
					this.m_targetCommand.SetQuery(this.PrepareQuery(true, true, "ExecuteReader"));
					IDataReader dataReader2 = this.m_targetCommand.ExecuteReader(behavior);
					if (dataReader2 == null)
					{
						SQEAssert.AssertFalseAndThrow("Target data reader is null.", Array.Empty<object>());
					}
					if (dataReader2 is IDataReaderExtension)
					{
						dataReader = new SemanticQueryCommandWrapper.DataReaderExtensionWrapper((IDataReaderExtension)dataReader2);
					}
					else
					{
						dataReader = new SemanticQueryCommandWrapper.DataReaderWrapper(dataReader2);
					}
				}
			}
			catch (Exception ex)
			{
				throw SemanticQueryCommandWrapper.ProcessExecutionException(ex);
			}
			return dataReader;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004DE4 File Offset: 0x00002FE4
		public IDataParameter CreateParameter()
		{
			return new SemanticQueryCommandWrapper.DataParameter();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004DEB File Offset: 0x00002FEB
		public void Cancel()
		{
			this.m_targetCommand.Cancel();
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00004DF8 File Offset: 0x00002FF8
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00004E00 File Offset: 0x00003000
		public string CommandText
		{
			get
			{
				return this.m_commandText;
			}
			set
			{
				this.m_commandText = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00004E09 File Offset: 0x00003009
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00004E16 File Offset: 0x00003016
		public int CommandTimeout
		{
			get
			{
				return this.m_targetCommand.CommandTimeout;
			}
			set
			{
				this.m_targetCommand.CommandTimeout = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004B5D File Offset: 0x00002D5D
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00004E24 File Offset: 0x00003024
		public CommandType CommandType
		{
			get
			{
				return CommandType.Text;
			}
			set
			{
				if (value != CommandType.Text)
				{
					throw new SemanticQueryEngineException(SR.InvalidCommandType);
				}
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004E35 File Offset: 0x00003035
		public IDataParameterCollection Parameters
		{
			get
			{
				if (this.m_parameters == null)
				{
					this.m_parameters = new SemanticQueryCommandWrapper.DataParameterCollection();
				}
				return this.m_parameters;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004E50 File Offset: 0x00003050
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00004E5D File Offset: 0x0000305D
		public IDbTransaction Transaction
		{
			get
			{
				return this.m_targetCommand.Transaction;
			}
			set
			{
				this.m_targetCommand.Transaction = value;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004E6B File Offset: 0x0000306B
		public void Dispose()
		{
			this.m_targetCommand.Dispose();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004E78 File Offset: 0x00003078
		public IDataParameterCollection GetParameters()
		{
			SemanticQueryCommandWrapper.DataParameterCollection dataParameterCollection = new SemanticQueryCommandWrapper.DataParameterCollection();
			foreach (Parameter parameter in this.PrepareQuery(false, false, "IDbCommandAnalysis.GetParameters").GetAllParameters())
			{
				dataParameterCollection.Add(new SemanticQueryCommandWrapper.DataParameter(parameter.Name));
			}
			return dataParameterCollection;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004EE4 File Offset: 0x000030E4
		string IDbCommandRewriter.RewrittenCommandText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.WriteSemanticQueryToString(this.PrepareQuery(false, true, "get_RewrittenCommandText"), stringBuilder, false);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004F14 File Offset: 0x00003114
		private SemanticQuery PrepareQuery(bool normalize, bool replaceParameterRefs, string operation)
		{
			SemanticQuery semanticQuery = null;
			using (XmlReader xmlReader = Microsoft.ReportingServices.Common.XmlRWFactory.CreateReader(new StringReader(this.m_commandText ?? string.Empty)))
			{
				QueryCompilationOptions queryCompilationOptions = QueryCompilationOptions.Subset;
				if (replaceParameterRefs)
				{
					queryCompilationOptions |= QueryCompilationOptions.ReplaceParameterRefs | QueryCompilationOptions.ResolveStaticFunctions;
				}
				semanticQuery = this.CompileQuery(this.m_userModel, xmlReader, this.CreateParameterValuesDictionary(), queryCompilationOptions, !normalize, operation);
			}
			if (normalize)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (XmlWriter xmlWriter = Microsoft.ReportingServices.Common.XmlRWFactory.CreateWriter(memoryStream))
					{
						semanticQuery.WriteTo(xmlWriter);
						xmlWriter.Flush();
						memoryStream.Position = 0L;
						using (XmlReader xmlReader2 = Microsoft.ReportingServices.Common.XmlRWFactory.CreateReader(memoryStream))
						{
							semanticQuery = this.CompileQuery(this.m_fullModel, xmlReader2, null, QueryCompilationOptions.ResolveStaticFunctions | QueryCompilationOptions.Normalize, true, operation);
						}
					}
				}
			}
			return semanticQuery;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005008 File Offset: 0x00003208
		private SemanticQuery CompileQuery(SemanticModel model, XmlReader queryXml, IDictionary<string, object> parameterValues, QueryCompilationOptions qco, bool traceQuery, string operation)
		{
			ValidationException ex = null;
			SemanticQuery semanticQuery;
			ValidationMessageCollection validationMessageCollection;
			try
			{
				semanticQuery = new SemanticQuery(model);
				validationMessageCollection = semanticQuery.Compile(queryXml, qco, parameterValues, this.m_userID, this.m_userCulture, new DateTime?(this.m_now), (ModelItem item) => this.m_userModel.LookupItemByID(item.ID) != null);
			}
			catch (ValidationException ex2)
			{
				ex = ex2;
				validationMessageCollection = ex2.Messages;
				semanticQuery = null;
			}
			catch (Exception ex3)
			{
				this.TracePrepareQuery(operation, null, null, ex3);
				throw;
			}
			if (validationMessageCollection.HasErrors)
			{
				this.TracePrepareQuery(operation, semanticQuery, validationMessageCollection, null);
				throw new SemanticQueryEngineException(SR.SemanticQueryCompilationError(validationMessageCollection.FirstError.ToString()));
			}
			if (ex != null)
			{
				this.TracePrepareQuery(operation, semanticQuery, null, ex);
				throw new SemanticQueryEngineException(SR.SemanticQueryCompilationError(ex.Message));
			}
			if (traceQuery)
			{
				this.TracePrepareQuery(operation, semanticQuery, validationMessageCollection, null);
			}
			return semanticQuery;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000050DC File Offset: 0x000032DC
		private IDataReader CreateSchemaOnlyDataReader(SemanticQuery query)
		{
			SemanticQueryCommandWrapper.SchemaOnlyDataReader schemaOnlyDataReader = new SemanticQueryCommandWrapper.SchemaOnlyDataReader();
			foreach (Expression expression in query.GetAllResultExpressions())
			{
				schemaOnlyDataReader.AddField(expression.Name, expression.GetResultType().DataType);
			}
			return schemaOnlyDataReader;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005144 File Offset: 0x00003344
		private IDictionary<string, object> CreateParameterValuesDictionary()
		{
			if (this.m_parameters == null || this.m_parameters.Count == 0)
			{
				return null;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>(this.m_parameters.Count);
			foreach (object obj in this.m_parameters)
			{
				SemanticQueryCommandWrapper.DataParameter dataParameter = (SemanticQueryCommandWrapper.DataParameter)obj;
				dictionary.Add(dataParameter.ParameterName, dataParameter.GetValue());
			}
			return dictionary;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000051D4 File Offset: 0x000033D4
		private static Exception ProcessExecutionException(Exception e)
		{
			if (e is DbException)
			{
				return new SemanticQueryEngineException(SR.ErrorExecutingSemanticQuery(e.Message));
			}
			return new SemanticQueryEngineException(SR.ErrorExecutingSemanticQuery(""), e);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005200 File Offset: 0x00003400
		private void TracePrepareQuery(string operation, SemanticQuery query, ValidationMessageCollection messages, Exception e)
		{
			if (messages == null && e == null)
			{
				return;
			}
			try
			{
				bool flag = false;
				TraceLevel traceLevel = TraceLevel.Verbose;
				if (this.m_traceLog != null && (this.m_traceLog.TraceVerbose || (this.m_traceLog.TraceWarning && messages != null && messages.Count > 0 && !messages.HasErrors) || (this.m_traceLog.TraceError && (e != null || messages.HasErrors))))
				{
					flag = true;
					if ((messages != null && messages.Count > 0) || e != null)
					{
						traceLevel = ((e != null || messages.HasErrors) ? TraceLevel.Error : TraceLevel.Warning);
					}
				}
				if (flag)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat("{0}\\PrepareQuery", operation);
					stringBuilder.Append("\r\n");
					stringBuilder.Append("Query = ");
					stringBuilder.Append("\r\n");
					if (query != null)
					{
						this.WriteSemanticQueryToString(query, stringBuilder, true);
					}
					else
					{
						stringBuilder.Append(this.m_commandText);
					}
					if (this.m_parameters != null && this.m_parameters.Count > 0)
					{
						foreach (object obj in this.m_parameters)
						{
							IDataParameter dataParameter = (IDataParameter)obj;
							stringBuilder.Append("\r\n");
							stringBuilder.AppendFormat("Query parameter: {0} = {1}", dataParameter.ParameterName, dataParameter.Value);
						}
					}
					stringBuilder.Append("\r\n");
					if (query != null && messages != null && messages.Count == 0)
					{
						stringBuilder.Append("Query has been compiled successfully.");
					}
					else if (query != null && messages != null && messages.Count > 0 && !messages.HasErrors)
					{
						stringBuilder.Append("Query has been compiled with warnings:");
					}
					else
					{
						stringBuilder.Append("Query compilation failed with errors:");
					}
					if (e != null)
					{
						stringBuilder.Append("\r\n");
						stringBuilder.AppendFormat("Exception: {0}", e.ToString());
					}
					else
					{
						for (int i = 0; i < messages.Count; i++)
						{
							stringBuilder.Append("\r\n");
							stringBuilder.Append(messages[i].ToString());
						}
					}
					this.m_traceLog.WriteTrace(stringBuilder.ToString(), traceLevel);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005458 File Offset: 0x00003658
		private void WriteSemanticQueryToString(SemanticQuery query, StringBuilder sb, bool verbose)
		{
			XmlWriterSettings writerSettings = Microsoft.ReportingServices.Common.XmlRWFactory.GetWriterSettings();
			writerSettings.Indent = true;
			using (XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(sb, CultureInfo.InvariantCulture), writerSettings))
			{
				query.WriteTo(xmlWriter, verbose ? ModelingSerializationOptions.NameComments : ModelingSerializationOptions.None);
				xmlWriter.Flush();
			}
		}

		// Token: 0x0400004A RID: 74
		private readonly SemanticModel m_userModel;

		// Token: 0x0400004B RID: 75
		private readonly SemanticModel m_fullModel;

		// Token: 0x0400004C RID: 76
		private readonly ISemanticQueryCommand m_targetCommand;

		// Token: 0x0400004D RID: 77
		private string m_commandText = "";

		// Token: 0x0400004E RID: 78
		private SemanticQueryCommandWrapper.DataParameterCollection m_parameters;

		// Token: 0x0400004F RID: 79
		private readonly string m_userID;

		// Token: 0x04000050 RID: 80
		private readonly string m_userCulture;

		// Token: 0x04000051 RID: 81
		private readonly DateTime m_now = DateTime.Now;

		// Token: 0x04000052 RID: 82
		private readonly ITraceLog m_traceLog;

		// Token: 0x04000053 RID: 83
		private static int m_commandIDCounter;

		// Token: 0x020000AA RID: 170
		internal sealed class DataParameter : IDataMultiValueParameter, IDataParameter
		{
			// Token: 0x06000672 RID: 1650 RVA: 0x0001A6EF File Offset: 0x000188EF
			internal DataParameter()
			{
			}

			// Token: 0x06000673 RID: 1651 RVA: 0x0001A702 File Offset: 0x00018902
			internal DataParameter(string name)
			{
				this.m_name = name;
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001A71C File Offset: 0x0001891C
			// (set) Token: 0x06000675 RID: 1653 RVA: 0x0001A724 File Offset: 0x00018924
			public string ParameterName
			{
				get
				{
					return this.m_name;
				}
				set
				{
					this.m_name = value;
				}
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x06000676 RID: 1654 RVA: 0x0001A72D File Offset: 0x0001892D
			// (set) Token: 0x06000677 RID: 1655 RVA: 0x0001A735 File Offset: 0x00018935
			public object Value
			{
				get
				{
					return this.m_value;
				}
				set
				{
					this.m_value = value;
					this.m_values = null;
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x06000678 RID: 1656 RVA: 0x0001A745 File Offset: 0x00018945
			// (set) Token: 0x06000679 RID: 1657 RVA: 0x0001A74D File Offset: 0x0001894D
			public object[] Values
			{
				get
				{
					return this.m_values;
				}
				set
				{
					this.m_values = value;
					this.m_value = null;
				}
			}

			// Token: 0x0600067A RID: 1658 RVA: 0x0001A75D File Offset: 0x0001895D
			internal object GetValue()
			{
				return this.m_value ?? this.m_values;
			}

			// Token: 0x04000316 RID: 790
			private string m_name = "";

			// Token: 0x04000317 RID: 791
			private object m_value;

			// Token: 0x04000318 RID: 792
			private object[] m_values;
		}

		// Token: 0x020000AB RID: 171
		internal sealed class DataParameterCollection : IDataParameterCollection, IEnumerable
		{
			// Token: 0x0600067B RID: 1659 RVA: 0x0001A76F File Offset: 0x0001896F
			internal DataParameterCollection()
			{
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001A782 File Offset: 0x00018982
			internal int Count
			{
				get
				{
					return this.m_list.Count;
				}
			}

			// Token: 0x0600067D RID: 1661 RVA: 0x0001A790 File Offset: 0x00018990
			internal void AddRange(IDataParameterCollection parameters)
			{
				if (parameters != null)
				{
					foreach (object obj in parameters)
					{
						IDataParameter dataParameter = (IDataParameter)obj;
						this.Add(dataParameter);
					}
				}
			}

			// Token: 0x0600067E RID: 1662 RVA: 0x0001A7E8 File Offset: 0x000189E8
			public int Add(IDataParameter parameter)
			{
				using (List<IDataParameter>.Enumerator enumerator = this.m_list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.ParameterName == parameter.ParameterName)
						{
							throw new SemanticQueryEngineException(SR.DuplicateParameterName(parameter.ParameterName));
						}
					}
				}
				this.m_list.Add(parameter);
				return this.m_list.Count - 1;
			}

			// Token: 0x0600067F RID: 1663 RVA: 0x0001A870 File Offset: 0x00018A70
			public IEnumerator GetEnumerator()
			{
				return this.m_list.GetEnumerator();
			}

			// Token: 0x04000319 RID: 793
			private List<IDataParameter> m_list = new List<IDataParameter>();
		}

		// Token: 0x020000AC RID: 172
		private sealed class SchemaOnlyDataReader : IDataReader, IDisposable
		{
			// Token: 0x06000680 RID: 1664 RVA: 0x0001A882 File Offset: 0x00018A82
			internal SchemaOnlyDataReader()
			{
			}

			// Token: 0x06000681 RID: 1665 RVA: 0x0001A8A0 File Offset: 0x00018AA0
			internal void AddField(string name, DataType modelingType)
			{
				this.m_ordinals.Add(name, this.m_fields.Count);
				this.m_fields.Add(new SemanticQueryCommandWrapper.SchemaOnlyDataReader.NameType(name, DataTypeMapper.TranslateDataType(modelingType)));
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001A8D0 File Offset: 0x00018AD0
			int IDataReader.FieldCount
			{
				get
				{
					return this.m_fields.Count;
				}
			}

			// Token: 0x06000683 RID: 1667 RVA: 0x0001A8DD File Offset: 0x00018ADD
			Type IDataReader.GetFieldType(int fieldIndex)
			{
				this.CheckFieldIndex(fieldIndex);
				return this.m_fields[fieldIndex].Type;
			}

			// Token: 0x06000684 RID: 1668 RVA: 0x0001A8F7 File Offset: 0x00018AF7
			string IDataReader.GetName(int fieldIndex)
			{
				this.CheckFieldIndex(fieldIndex);
				return this.m_fields[fieldIndex].Name;
			}

			// Token: 0x06000685 RID: 1669 RVA: 0x0001A914 File Offset: 0x00018B14
			int IDataReader.GetOrdinal(string fieldName)
			{
				int num;
				if (this.m_ordinals.TryGetValue(fieldName, out num))
				{
					return num;
				}
				return -1;
			}

			// Token: 0x06000686 RID: 1670 RVA: 0x0001A934 File Offset: 0x00018B34
			object IDataReader.GetValue(int fieldIndex)
			{
				this.CheckFieldIndex(fieldIndex);
				return DBNull.Value;
			}

			// Token: 0x06000687 RID: 1671 RVA: 0x00004555 File Offset: 0x00002755
			bool IDataReader.Read()
			{
				return false;
			}

			// Token: 0x06000688 RID: 1672 RVA: 0x00003FB8 File Offset: 0x000021B8
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000689 RID: 1673 RVA: 0x0001A942 File Offset: 0x00018B42
			private void CheckFieldIndex(int fieldIndex)
			{
				if (fieldIndex < 0 || fieldIndex >= this.m_fields.Count)
				{
					throw SQEAssert.AssertFalseAndThrow(new ArgumentOutOfRangeException("fieldIndex"));
				}
			}

			// Token: 0x0400031A RID: 794
			private readonly Dictionary<string, int> m_ordinals = new Dictionary<string, int>();

			// Token: 0x0400031B RID: 795
			private readonly List<SemanticQueryCommandWrapper.SchemaOnlyDataReader.NameType> m_fields = new List<SemanticQueryCommandWrapper.SchemaOnlyDataReader.NameType>();

			// Token: 0x020000DC RID: 220
			private sealed class NameType
			{
				// Token: 0x06000789 RID: 1929 RVA: 0x0001D157 File Offset: 0x0001B357
				internal NameType(string name, Type type)
				{
					this.Name = name;
					this.Type = type;
				}

				// Token: 0x040003D9 RID: 985
				internal readonly string Name;

				// Token: 0x040003DA RID: 986
				internal readonly Type Type;
			}
		}

		// Token: 0x020000AD RID: 173
		private class DataReaderWrapper : IDataReader, IDisposable
		{
			// Token: 0x0600068A RID: 1674 RVA: 0x0001A966 File Offset: 0x00018B66
			internal DataReaderWrapper(IDataReader targetDataReader)
			{
				this.m_targetDataReader = targetDataReader;
			}

			// Token: 0x0600068B RID: 1675 RVA: 0x0001A975 File Offset: 0x00018B75
			public string GetName(int fieldIndex)
			{
				return this.m_targetDataReader.GetName(fieldIndex);
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x0001A983 File Offset: 0x00018B83
			public int GetOrdinal(string fieldName)
			{
				return this.m_targetDataReader.GetOrdinal(fieldName);
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001A991 File Offset: 0x00018B91
			public int FieldCount
			{
				get
				{
					return this.m_targetDataReader.FieldCount;
				}
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x0001A9A0 File Offset: 0x00018BA0
			public bool Read()
			{
				bool flag;
				try
				{
					flag = this.m_targetDataReader.Read();
				}
				catch (Exception ex)
				{
					throw SemanticQueryCommandWrapper.ProcessExecutionException(ex);
				}
				return flag;
			}

			// Token: 0x0600068F RID: 1679 RVA: 0x0001A9D4 File Offset: 0x00018BD4
			public Type GetFieldType(int fieldIndex)
			{
				return this.m_targetDataReader.GetFieldType(fieldIndex);
			}

			// Token: 0x06000690 RID: 1680 RVA: 0x0001A9E4 File Offset: 0x00018BE4
			public object GetValue(int fieldIndex)
			{
				object value;
				try
				{
					value = this.m_targetDataReader.GetValue(fieldIndex);
				}
				catch (Exception ex)
				{
					throw SemanticQueryCommandWrapper.ProcessExecutionException(ex);
				}
				return value;
			}

			// Token: 0x06000691 RID: 1681 RVA: 0x0001AA18 File Offset: 0x00018C18
			public void Dispose()
			{
				this.m_targetDataReader.Dispose();
			}

			// Token: 0x0400031C RID: 796
			private readonly IDataReader m_targetDataReader;
		}

		// Token: 0x020000AE RID: 174
		private sealed class DataReaderExtensionWrapper : SemanticQueryCommandWrapper.DataReaderWrapper, IDataReaderExtension, IDataReader, IDisposable
		{
			// Token: 0x06000692 RID: 1682 RVA: 0x0001AA25 File Offset: 0x00018C25
			internal DataReaderExtensionWrapper(IDataReaderExtension targetDataReader)
				: base(targetDataReader)
			{
				this.m_targetDataReader = targetDataReader;
			}

			// Token: 0x06000693 RID: 1683 RVA: 0x0001AA35 File Offset: 0x00018C35
			public bool IsAggregationField(int index)
			{
				return this.m_targetDataReader.IsAggregationField(index);
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x06000694 RID: 1684 RVA: 0x0001AA43 File Offset: 0x00018C43
			public bool IsAggregateRow
			{
				get
				{
					return this.m_targetDataReader.IsAggregateRow;
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001AA50 File Offset: 0x00018C50
			public int AggregationFieldCount
			{
				get
				{
					return this.m_targetDataReader.AggregationFieldCount;
				}
			}

			// Token: 0x0400031D RID: 797
			private readonly IDataReaderExtension m_targetDataReader;
		}
	}
}
