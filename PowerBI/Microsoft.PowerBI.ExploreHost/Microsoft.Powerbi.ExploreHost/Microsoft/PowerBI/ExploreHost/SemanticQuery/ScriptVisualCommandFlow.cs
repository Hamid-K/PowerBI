using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;
using Microsoft.PowerBI.ExploreServiceCommon;
using Microsoft.PowerBI.ExploreServiceCommon.Interfaces;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ExploreHost.SemanticQuery
{
	// Token: 0x02000044 RID: 68
	internal sealed class ScriptVisualCommandFlow : ITransformFlow
	{
		// Token: 0x06000234 RID: 564 RVA: 0x00006D65 File Offset: 0x00004F65
		public ScriptVisualCommandFlow(SemanticQueryDataShapeCommand command, ScriptVisualCommand scriptVisualTransform, IScriptHandler scriptHandler)
		{
			this.m_scriptVisualCommand = scriptVisualTransform;
			this.m_scriptHandler = scriptHandler;
			this.m_command = command;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006D84 File Offset: 0x00004F84
		public bool Run(Stream dataShapeResultStream, ref QueryBindingDescriptor bindingDescriptor)
		{
			bool flag;
			try
			{
				flag = this.RunInternal(dataShapeResultStream, ref bindingDescriptor);
			}
			catch (ScriptHandlerException ex)
			{
				this.SendErrorTelemetry(ex.InnerException ?? ex);
				throw;
			}
			catch (Exception ex2)
			{
				this.SendErrorTelemetry(ex2);
				throw;
			}
			return flag;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00006DD8 File Offset: 0x00004FD8
		private void SendErrorTelemetry(Exception e)
		{
			TelemetryService.Instance.Log(new PBIWinScriptVisualTransformFailed(e.GetType().FullName, "ScriptVisualCommandFlow-RunScriptError"));
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006DFC File Offset: 0x00004FFC
		private bool RunInternal(Stream dataShapeResultStream, ref QueryBindingDescriptor bindingDescriptor)
		{
			IEnumerable<Tuple<string, string>> enumerable = ExecuteSemanticQueryResultStreamReader.CreatePrimarySelectsMap(bindingDescriptor, this.m_scriptVisualCommand.ScriptInput, this.m_command);
			dataShapeResultStream.Position = 0L;
			Stopwatch stopwatch = Stopwatch.StartNew();
			long length = dataShapeResultStream.Length;
			using (ExecuteSemanticQueryResultStreamReader executeSemanticQueryResultStreamReader = new ExecuteSemanticQueryResultStreamReader(enumerable, bindingDescriptor, dataShapeResultStream))
			{
				bool flag = true;
				try
				{
					if (executeSemanticQueryResultStreamReader.IsODataError)
					{
						return false;
					}
					string text = null;
					IList<InputColumn> list = null;
					IList<IDataParameter> list2 = null;
					if (this.m_scriptVisualCommand.ScriptInput != null)
					{
						if (!string.IsNullOrWhiteSpace(this.m_scriptVisualCommand.ScriptInput.VariableName))
						{
							text = this.m_scriptVisualCommand.ScriptInput.VariableName;
						}
						if (this.m_scriptVisualCommand.ScriptInput.Columns != null)
						{
							if (!this.m_scriptVisualCommand.ScriptInput.Columns.Any((ScriptInputColumn ic) => ic == null || string.IsNullOrWhiteSpace(ic.Role)))
							{
								list = this.m_scriptVisualCommand.ScriptInput.Columns.Select((ScriptInputColumn c) => new InputColumn
								{
									Role = c.Role,
									Name = c.Name
								}).ToList<InputColumn>();
							}
						}
						if (this.m_scriptVisualCommand.ScriptInput.Parameters != null)
						{
							list2 = this.m_scriptVisualCommand.ScriptInput.Parameters.Where((ScriptInputParameter param) => param != null && !string.IsNullOrWhiteSpace(param.ObjectName) && !string.IsNullOrWhiteSpace(param.PropertyName)).Select(delegate(ScriptInputParameter param)
							{
								param.ParameterName = param.ObjectName + this.m_scriptHandler.VariableNameWordsSeparator + param.PropertyName;
								return param;
							}).ToList<IDataParameter>();
						}
					}
					ScriptHandlerOptions scriptHandlerOptions = new ScriptHandlerOptions
					{
						Script = this.m_scriptVisualCommand.Script,
						InputVariableName = text,
						Reader = executeSemanticQueryResultStreamReader,
						InputColumns = list,
						InputParameters = list2,
						ViewportWidthPx = this.m_scriptVisualCommand.ViewportWidthPx,
						ViewportHeightPx = this.m_scriptVisualCommand.ViewportHeightPx,
						ScriptOutputType = this.m_scriptVisualCommand.ScriptOutputType
					};
					Stream stream;
					try
					{
						stream = this.m_scriptHandler.GenerateVisual(scriptHandlerOptions);
					}
					catch (Exception ex)
					{
						if (ex.IsStoppingException())
						{
							throw;
						}
						ex.SetContainsPII();
						throw;
					}
					stopwatch.Stop();
					bindingDescriptor = new QueryBindingDescriptor
					{
						Version = new int?(1),
						ScriptVisualBinding = new ScriptVisualBinding
						{
							PayloadCalculationId = "G0"
						},
						Limits = bindingDescriptor.Limits
					};
					dataShapeResultStream.Position = 0L;
					dataShapeResultStream.SetLength(0L);
					this.WriteScriptVisualDataShapeResult(dataShapeResultStream, stream, "G0", executeSemanticQueryResultStreamReader.ExceededDataLimitsIds, executeSemanticQueryResultStreamReader.DataShapeId);
					flag = false;
				}
				finally
				{
					TelemetryService.Instance.Log(new PBIWinScriptVisualTransform(executeSemanticQueryResultStreamReader.RowsRead, length, executeSemanticQueryResultStreamReader.ParsingTimeMilliseconds, executeSemanticQueryResultStreamReader.TotalTimeMilliseconds, stopwatch.ElapsedMilliseconds, TelemetryService.Instance.Root.Id, flag));
				}
			}
			return false;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007104 File Offset: 0x00005304
		private void WriteScriptVisualDataShapeResult(Stream stream, Stream imageStream, string imageObjectId, IList<string> exceededDataLimitsIds, string dataShapeId)
		{
			using (JsonScriptVisualResultWriter jsonScriptVisualResultWriter = new JsonScriptVisualResultWriter(new StreamWriter(stream)))
			{
				jsonScriptVisualResultWriter.WriteResult(dataShapeId, imageStream, imageObjectId, exceededDataLimitsIds);
			}
		}

		// Token: 0x040000D6 RID: 214
		private const string PayloadCalculationId = "G0";

		// Token: 0x040000D7 RID: 215
		private readonly SemanticQueryDataShapeCommand m_command;

		// Token: 0x040000D8 RID: 216
		private readonly ScriptVisualCommand m_scriptVisualCommand;

		// Token: 0x040000D9 RID: 217
		private readonly IScriptHandler m_scriptHandler;
	}
}
