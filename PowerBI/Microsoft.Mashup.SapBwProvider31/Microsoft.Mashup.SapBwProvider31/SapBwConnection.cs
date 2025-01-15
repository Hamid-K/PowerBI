using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200002B RID: 43
	public sealed class SapBwConnection : DbConnection, ISapBwMicrosoftConnection
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000905A File Offset: 0x0000725A
		public SapBwConnection(ISapBwProvider provider)
		{
			this.provider = provider;
			this.connectionStringBuilder = new SapBwConnectionStringBuilder();
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00009074 File Offset: 0x00007274
		public ISapBwProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000907C File Offset: 0x0000727C
		public SapBwModuleHelper Helper
		{
			get
			{
				return this.helper;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00009084 File Offset: 0x00007284
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000909C File Offset: 0x0000729C
		public override string ConnectionString
		{
			get
			{
				if (this.connectionStringBuilder == null)
				{
					return null;
				}
				return this.connectionStringBuilder.ConnectionString;
			}
			set
			{
				this.connectionStringBuilder.ConnectionString = value;
				this.parameters = null;
				this.traceEnabled = false;
				this.verboseEnabled = false;
				this.fileTracer = null;
				this.batchSize = 50000;
				this.sapBwUser = null;
				this.helper = null;
				this.resource = null;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000090F1 File Offset: 0x000072F1
		public SapBwDecimalNotation DecimalNotation
		{
			get
			{
				return this.SapBwUser.DecimalNotation;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000090FE File Offset: 0x000072FE
		public SapBwUser SapBwUser
		{
			get
			{
				if (this.sapBwUser == null)
				{
					this.sapBwUser = SapBwUser.New(this);
				}
				return this.sapBwUser;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000911C File Offset: 0x0000731C
		private IResource Resource
		{
			get
			{
				if (this.resource == null)
				{
					string text = SapBwRouterString.BuildRouterStringOrNull(this.parameters);
					DataSourceLocation dataSourceLocation;
					if (!this.parameters.ContainsKey("MSHOST"))
					{
						SapBwOlapDataSourceLocation sapBwOlapDataSourceLocation = new SapBwOlapDataSourceLocation();
						sapBwOlapDataSourceLocation.Server = text ?? this.parameters["ASHOST"];
						sapBwOlapDataSourceLocation.SystemNumber = this.parameters["SYSNR"];
						dataSourceLocation = sapBwOlapDataSourceLocation;
						sapBwOlapDataSourceLocation.ClientId = this.parameters["CLIENT"];
					}
					else
					{
						SapBwOlapDataSourceLocation sapBwOlapDataSourceLocation2 = new SapBwOlapDataSourceLocation();
						sapBwOlapDataSourceLocation2.Server = text ?? this.parameters["MSHOST"];
						string text2;
						sapBwOlapDataSourceLocation2.SystemId = (this.parameters.TryGetValue("SYSID", out text2) ? text2 : this.GetRequiredKeyword("SystemID"));
						sapBwOlapDataSourceLocation2.ClientId = this.parameters["CLIENT"];
						dataSourceLocation = sapBwOlapDataSourceLocation2;
						sapBwOlapDataSourceLocation2.LogonGroup = this.parameters["GROUP"];
					}
					dataSourceLocation.TryGetResource(out this.resource);
				}
				return this.resource;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009226 File Offset: 0x00007426
		internal IDestination Destination
		{
			get
			{
				return this.destination;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000922E File Offset: 0x0000742E
		public override string Database
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00009235 File Offset: 0x00007435
		public override string DataSource
		{
			get
			{
				this.EnsureInitialized();
				if (this.parameters != null)
				{
					return this.parameters["NAME"];
				}
				return null;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00009257 File Offset: 0x00007457
		public override string ServerVersion
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000925E File Offset: 0x0000745E
		public override ConnectionState State
		{
			get
			{
				return this.connectionState;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00009266 File Offset: 0x00007466
		public int BatchSize
		{
			get
			{
				return this.batchSize;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000926E File Offset: 0x0000746E
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00009276 File Offset: 0x00007476
		public Func<string, IDisposable> ImpersonationWrapper { get; set; }

		// Token: 0x06000223 RID: 547 RVA: 0x0000927F File Offset: 0x0000747F
		private void SetParameter(List<string> nameParts, string key, string value)
		{
			this.parameters[key] = value;
			nameParts.Add(string.Format(CultureInfo.InvariantCulture, "{0}={1}", key, value));
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000092A8 File Offset: 0x000074A8
		private void EnsureInitialized()
		{
			if (string.IsNullOrEmpty(this.ConnectionString))
			{
				return;
			}
			if (this.parameters == null)
			{
				List<string> list = new List<string>();
				this.parameters = new RfcConfigParameters();
				this.parameters["MAX_POOL_SIZE"] = "10";
				this.parameters["IDLE_TIMEOUT"] = "600";
				this.SetParameter(list, "LANG", this.GetRequiredKeyword("LANG"));
				this.SetParameter(list, "CLIENT", this.GetRequiredKeyword("CLIENT"));
				string text;
				if (this.connectionStringBuilder.TryGetString("SAPROUTER", out text))
				{
					this.SetParameter(list, "SAPROUTER", text);
				}
				object obj;
				if (this.connectionStringBuilder.TryGetValue("SYSNR", out obj))
				{
					this.SetParameter(list, "ASHOST", this.GetRequiredKeyword("ASHOST"));
					string text2;
					if (this.connectionStringBuilder.TryGetString("ASSERV", out text2))
					{
						this.SetParameter(list, "ASSERV", text2);
					}
					this.SetParameter(list, "SYSNR", obj as string);
				}
				else
				{
					this.SetParameter(list, "MSHOST", this.GetRequiredKeyword("MessageServer"));
					string text3;
					if (this.connectionStringBuilder.TryGetString("MSSERV", out text3))
					{
						this.SetParameter(list, "MSSERV", text3);
					}
					else
					{
						this.SetParameter(list, "SYSID", this.GetRequiredKeyword("SystemID"));
					}
					this.SetParameter(list, "GROUP", this.GetRequiredKeyword("LogonGroup"));
				}
				this.serverLevelCacheKeyBase = string.Join(";", list);
				string text4;
				string text5;
				if (this.connectionStringBuilder.TryGetString("USER", out text4) && this.connectionStringBuilder.TryGetString("PASSWD", out text5))
				{
					this.SetParameter(list, "USER", text4);
					this.parameters["PASSWD"] = text5;
				}
				else
				{
					string text6;
					string text7;
					if (!this.connectionStringBuilder.TryGetSncLibrary(out text6) || !this.connectionStringBuilder.TryGetString("SNCPartnerName", out text7))
					{
						throw SapBwException.NewAuthorizationException(Resources.MissingAuthenticationParameters);
					}
					this.SetParameter(list, "SNC_MODE", "1");
					this.SetParameter(list, "SNC_LIB", text6);
					this.SetParameter(list, "SNC_PARTNERNAME", text7);
					this.SetParameter(list, "SNC_QOP", "8");
				}
				this.parameters["NAME"] = string.Join(";", list);
				this.connectionStringBuilder.TryGetBool("TraceEnabled", out this.traceEnabled);
				this.connectionStringBuilder.TryGetBool("VerboseEnabled", out this.verboseEnabled);
				if (this.traceEnabled || this.verboseEnabled)
				{
					uint num = (this.verboseEnabled ? 7U : 1U);
					this.parameters["TRACE"] = num.ToString(CultureInfo.InvariantCulture);
				}
				string text8;
				if (this.connectionStringBuilder.TryGetString("DEBUGDIRECTORY", out text8))
				{
					this.fileTracer = this.provider.GetFileTracerOrNull(text8);
				}
				int num2;
				if (this.connectionStringBuilder.TryGetInt("BatchSize", out num2) && num2 > 0)
				{
					this.batchSize = num2;
					return;
				}
				this.batchSize = 50000;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000095C8 File Offset: 0x000077C8
		public override void Open()
		{
			this.connectionState = ConnectionState.Connecting;
			if (this.connectionStringBuilder == null || string.IsNullOrEmpty(this.connectionStringBuilder.ConnectionString))
			{
				throw new SapBwException(Resources.MissingConnectionString);
			}
			this.EnsureInitialized();
			string text = this.parameters["NAME"];
			try
			{
				this.destination = this.provider.GetDestination(text, this.parameters, this.ImpersonationWrapper);
			}
			catch (Exception ex)
			{
				IResource resource;
				if (this.TryGetResource(out resource))
				{
					throw new SapBwModuleHelper(null, resource).WrapException(ex);
				}
				throw;
			}
			this.connectionState = ConnectionState.Open;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00009670 File Offset: 0x00007870
		private bool TryGetResource(out IResource safeResource)
		{
			bool flag;
			try
			{
				safeResource = this.Resource;
				flag = true;
			}
			catch (FileLoadException)
			{
				safeResource = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000096A4 File Offset: 0x000078A4
		public IRfcFunction GetFunction(string functionName, bool useNewBapis = true)
		{
			this.EnsureConnectionIsOpen();
			string text;
			if (useNewBapis && SapBwConnection.newBapis.TryGetValue(functionName, out text))
			{
				functionName = text;
			}
			IRfcFunction rfcFunction;
			try
			{
				rfcFunction = this.provider.CreateFunction(functionName, this.destination);
			}
			catch (RfcBaseException ex)
			{
				throw this.Helper.WrapException(SapBwException.FromException(ex));
			}
			if (this.verboseEnabled)
			{
				rfcFunction.AbapClassExceptionMode = 1;
			}
			return rfcFunction;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00009718 File Offset: 0x00007918
		private void InvokeFunctionInternal(IRfcFunction function, string traceKey, bool initStatefulSession, bool checkForErrors)
		{
			this.EnsureConnectionIsOpen();
			this.EnsureStatefulness(initStatefulSession);
			this.destination.InvokeFunction(function, traceKey);
			if (checkForErrors)
			{
				IRfcStructure structure = function.GetStructure("RETURN");
				string @string = structure["TYPE"].GetString();
				if (@string == "E" || @string == "A")
				{
					string string2 = structure["MESSAGE"].GetString();
					if (!string.IsNullOrWhiteSpace(string2))
					{
						Dictionary<string, string> dictionary = new Dictionary<string, string>();
						foreach (string text in SapBwConnection.returnStructureFields)
						{
							string string3 = structure[text].GetString();
							if (!string.IsNullOrEmpty(string3))
							{
								dictionary[text] = string3;
							}
						}
						dictionary["Command"] = traceKey;
						string text2;
						dictionary.TryGetValue("ID", out text2);
						string text3;
						dictionary.TryGetValue("NUMBER", out text3);
						string text4 = string.Join(" ", new string[] { text2, text3 });
						string text5;
						SapBwException.KnownErrors.TryGetValue(text4, out text5);
						throw this.helper.NewSapBwError(string2, dictionary, text4, text5);
					}
				}
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000986C File Offset: 0x00007A6C
		public void InvokeFunction(IRfcFunction function, bool initStatefulSession, SapBwCommand command, bool checkForErrors = false)
		{
			command.IncrementStep();
			bool? flag = null;
			Stopwatch sw = (this.traceEnabled ? Stopwatch.StartNew() : null);
			try
			{
				this.InvokeFunctionInternal(function, command.TraceKey, initStatefulSession, checkForErrors);
			}
			catch (Exception ex)
			{
				Exception ex5;
				Exception ex2 = ex5;
				Exception ex = ex2;
				this.helper.Trace("SapBwConnection/InvokeFunction/Exception", delegate(IHostTrace trace)
				{
					trace.Add("FunctionName", function.Metadata.Name, false);
					trace.Add(ex, true);
				});
				SapBwException ex3;
				if (SapBwException.TryExtractExceptionDetails(ex, out ex3))
				{
					flag = new bool?(true);
					throw this.helper.WrapException(ex3);
				}
				Exception ex4;
				if (this.helper.TryWrapException(ex, out ex4))
				{
					throw ex4;
				}
				throw;
			}
			finally
			{
				if (flag == null)
				{
					this.helper.Trace("SapBwConnection/InvokeFunction", delegate(IHostTrace trace)
					{
						trace.Add("FunctionName", function.Metadata.Name, false);
						if (sw != null)
						{
							sw.Stop();
							trace.Add("ElapsedMilliseconds", sw.ElapsedMilliseconds, false);
						}
					});
				}
				if (this.fileTracer != null)
				{
					this.fileTracer.TraceFunctionInvoke(function, command, flag);
				}
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009990 File Offset: 0x00007B90
		public bool TryInvokeStatelessFunction(IRfcFunction function, bool checkForErrors, string argument)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "{0}={1}", function.Metadata.Name, argument);
			int num = argument.IndexOf("|", StringComparison.Ordinal);
			string text2 = ((num == -1) ? argument : argument.Substring(0, num));
			string text3 = string.Format(CultureInfo.InvariantCulture, "{0}={1}", function.Metadata.Name, text2);
			bool flag = false;
			try
			{
				this.InvokeFunctionInternal(function, text, false, checkForErrors);
				flag = true;
			}
			catch (Exception ex)
			{
				Exception ex2;
				ex = ex2;
				this.helper.Trace("SapBwConnection/TryInvokeStatelessFunction/Exception", delegate(IHostTrace trace)
				{
					trace.Add("FunctionName", function.Metadata.Name, false);
					trace.Add(ex, true);
				});
				if (!this.helper.IsSafeException(ex))
				{
					throw;
				}
			}
			finally
			{
				if (this.fileTracer != null)
				{
					this.fileTracer.TraceStatelessFunction(function, Utils.GetStableHash(text), text3, flag ? "OK" : "EXC");
				}
			}
			return flag;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009AB0 File Offset: 0x00007CB0
		public override void ChangeDatabase(string databaseName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009AB8 File Offset: 0x00007CB8
		private void BeginContext()
		{
			if (this.destination != null)
			{
				try
				{
					this.destination.BeginContext();
				}
				catch (RfcInvalidStateException ex)
				{
					RfcInvalidStateException ex3;
					RfcInvalidStateException ex2 = ex3;
					RfcInvalidStateException ex = ex2;
					this.helper.Trace("SapBwConnection/BeginContext", delegate(IHostTrace trace)
					{
						trace.Add(ex, true);
					});
					throw this.helper.WrapException(SapBwException.FromException(ex));
				}
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009B2C File Offset: 0x00007D2C
		public void EndContext()
		{
			if (this.destination != null)
			{
				try
				{
					this.destination.EndContext();
				}
				catch (RfcInvalidStateException ex)
				{
					RfcInvalidStateException ex3;
					RfcInvalidStateException ex2 = ex3;
					RfcInvalidStateException ex = ex2;
					this.helper.Trace("SapBwConnection/EndContext", delegate(IHostTrace trace)
					{
						trace.Add(ex, true);
					});
				}
				finally
				{
					this.stateful = false;
				}
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009BA0 File Offset: 0x00007DA0
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009BA7 File Offset: 0x00007DA7
		protected override DbCommand CreateDbCommand()
		{
			return new SapBwCommand
			{
				Connection = this
			};
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009BB5 File Offset: 0x00007DB5
		private void EnsureConnectionIsOpen()
		{
			if (this.connectionState != ConnectionState.Open)
			{
				throw this.helper.NewDataSourceError(Resources.UnexpectedConnectionState(this.connectionState));
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009BE1 File Offset: 0x00007DE1
		private void EnsureStatefulness(bool initStatefulSession)
		{
			if (initStatefulSession && !this.stateful)
			{
				this.BeginContext();
				this.stateful = true;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00009BFC File Offset: 0x00007DFC
		public void EnsureHelper(SapBwCommand command)
		{
			if (this.helper == null)
			{
				object obj;
				this.helper = ((command.TryGetParameterValue("HELPER", out obj) && obj is SapBwModuleHelper) ? ((SapBwModuleHelper)obj) : new SapBwModuleHelper(null, this.Resource));
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009C44 File Offset: 0x00007E44
		private string GetRequiredKeyword(string keyword)
		{
			string text;
			if (this.connectionStringBuilder.TryGetString(keyword, out text))
			{
				return text;
			}
			throw new SapBwException(Resources.MissingRequiredKeyword(keyword));
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009C73 File Offset: 0x00007E73
		public void FileTrace(Action<IFileTracer> trace)
		{
			if (this.fileTracer != null)
			{
				trace(this.fileTracer);
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009C8C File Offset: 0x00007E8C
		public bool TryGetInfoObjectDetail(string infoObject, out IRfcStructure detail)
		{
			if (!string.IsNullOrEmpty(infoObject))
			{
				string cacheKey = this.GetCacheKey("BAPI_IOBJ_GETDETAIL", "DETAILS", infoObject, false);
				if (this.provider.Structures.TryGetValue(cacheKey, out detail))
				{
					return true;
				}
				IRfcFunction function = this.GetFunction("BAPI_IOBJ_GETDETAIL", false);
				function.SetValue("VERSION", 'A');
				function.SetValue("INFOOBJECT", infoObject);
				if (this.TryInvokeStatelessFunction(function, true, infoObject))
				{
					detail = function.GetStructure("DETAILS");
					if (detail != null)
					{
						this.provider.Structures[cacheKey] = detail;
						return true;
					}
				}
			}
			detail = null;
			return false;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00009D28 File Offset: 0x00007F28
		public bool TryGetInfoObjectType(string infoObject, out SapBwDataType dataType)
		{
			IRfcStructure rfcStructure;
			if (this.TryGetInfoObjectDetail(infoObject, out rfcStructure))
			{
				return MdxColumn.DataTypesByName.TryGetValue(rfcStructure["DATATP"].GetString(), out dataType);
			}
			dataType = SapBwDataType.Accp;
			return false;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00009D60 File Offset: 0x00007F60
		public bool TryGetUserName(out string username)
		{
			this.EnsureInitialized();
			return this.parameters.TryGetValue("USER", out username) && !string.IsNullOrEmpty(username);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009D88 File Offset: 0x00007F88
		public void TraceMessage(string method, Func<string> getMessage)
		{
			this.helper.Trace(method, delegate(IHostTrace trace)
			{
				trace.Add("Message", getMessage(), true);
			});
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00009DBC File Offset: 0x00007FBC
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			if (string.Equals(collectionName, "INFOOBJECTS", StringComparison.OrdinalIgnoreCase) && restrictionValues.Length != 0)
			{
				DataTable dataTable = new DataTable("INFOOBJECTS")
				{
					Locale = CultureInfo.InvariantCulture,
					MinimumCapacity = restrictionValues.Length
				};
				dataTable.Columns.Add("ReferenceCharacteristic", typeof(string));
				dataTable.Columns.Add("TextTableExists", typeof(bool));
				dataTable.Columns.Add("TextsAreTimeDependent", typeof(bool));
				dataTable.Columns.Add("MasterDataIsTimeDependent", typeof(bool));
				dataTable.Columns.Add("TextsAreLanguageIndependent", typeof(bool));
				dataTable.Columns.Add("ShortTextExists", typeof(bool));
				dataTable.Columns.Add("MediumTextExists", typeof(bool));
				dataTable.Columns.Add("LongTextExists", typeof(bool));
				dataTable.Columns.Add("AbapDataType", typeof(string));
				dataTable.Columns.Add("TimeIndependentMasterDataTable", typeof(string));
				dataTable.Columns.Add("TimeDependentMasterDataTable", typeof(string));
				dataTable.Columns.Add("TextTable", typeof(string));
				dataTable.Columns.Add("HierarchyTable", typeof(string));
				dataTable.Columns.Add("HierachyIntervalTable", typeof(string));
				dataTable.Columns.Add("ViewOfMasterDataTables", typeof(string));
				dataTable.Columns.Add("KeyFigureDecimals", typeof(int));
				dataTable.Columns.Add("KeyFigurePresentation", typeof(int));
				dataTable.Columns.Add("InfoObject", typeof(string));
				dataTable.Columns.Add("Type", typeof(string));
				dataTable.Columns.Add("CharacteristicReferencesAnother", typeof(bool));
				dataTable.Columns.Add("InternalLength", typeof(int));
				foreach (string text in restrictionValues)
				{
					IRfcStructure rfcStructure;
					if (this.TryGetInfoObjectDetail(text, out rfcStructure))
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow[0] = rfcStructure["CHABASNM"].GetString();
						dataRow[1] = Utils.ToBoolean(rfcStructure["TXTTABFL"].GetChar());
						dataRow[2] = Utils.ToBoolean(rfcStructure["TXTTIMFL"].GetChar());
						dataRow[3] = Utils.ToBoolean(rfcStructure["TIMDEPFL"].GetChar());
						dataRow[4] = Utils.ToBoolean(rfcStructure["NOLANGU"].GetChar());
						dataRow[5] = Utils.ToBoolean(rfcStructure["TXTSHFL"].GetChar());
						dataRow[6] = Utils.ToBoolean(rfcStructure["TXTMDFL"].GetChar());
						dataRow[7] = Utils.ToBoolean(rfcStructure["TXTLGFL"].GetChar());
						dataRow[8] = rfcStructure["DATATP"].GetString();
						dataRow[9] = rfcStructure["CHNTAB"].GetString();
						dataRow[10] = rfcStructure["CHTTAB"].GetString();
						dataRow[11] = rfcStructure["TXTTAB"].GetString();
						dataRow[12] = rfcStructure["HIETAB"].GetString();
						dataRow[13] = rfcStructure["HINTAB"].GetString();
						dataRow[14] = rfcStructure["CHKTAB"].GetString();
						dataRow[15] = Utils.ToInt(rfcStructure["KYFDECIM"].GetChar());
						dataRow[16] = Utils.ToInt(rfcStructure["KYFPRSNT"].GetChar());
						dataRow[17] = rfcStructure["INFOOBJECT"].GetString();
						dataRow[18] = rfcStructure["TYPE"].GetString();
						dataRow[19] = Utils.ToBoolean(rfcStructure["BCHREFFL"].GetChar());
						dataRow[20] = rfcStructure["INTLEN"].GetInt();
						dataTable.Rows.Add(dataRow);
						dataRow.AcceptChanges();
					}
				}
				dataTable.AcceptChanges();
				return dataTable;
			}
			return null;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A306 File Offset: 0x00008506
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000A30F File Offset: 0x0000850F
		protected override void Dispose(bool disposing)
		{
			this.connectionState = ConnectionState.Closed;
			if (disposing && this.stateful)
			{
				this.EndContext();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000A330 File Offset: 0x00008530
		public string GetCacheKey(string function, string structure, string argument, bool serverLevelCache = false)
		{
			return string.Join("|", new string[]
			{
				serverLevelCache ? this.serverLevelCacheKeyBase : this.parameters["NAME"],
				function,
				structure,
				argument
			});
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000A36D File Offset: 0x0000856D
		public RfcCustomRepository BuildCustomRepository(IEnumerable<string> functionNames)
		{
			this.EnsureConnectionIsOpen();
			return Repository.BuildCustomRepository(functionNames, this.destination);
		}

		// Token: 0x040000F4 RID: 244
		private const string SncModeDoNotApply = "0";

		// Token: 0x040000F5 RID: 245
		private const string SncModeApply = "1";

		// Token: 0x040000F6 RID: 246
		private static readonly Dictionary<string, string> newBapis = new Dictionary<string, string>
		{
			{ "BAPI_MDDATASET_CREATE_OBJECT", "RSR_MDX_CREATE_OBJECT" },
			{ "BAPI_MDDATASET_GET_AXIS_INFO", "RSR_MDX_GET_AXIS_INFO" },
			{ "BAPI_MDDATASET_GET_AXIS_DATA", "RSR_MDX_GET_AXIS_DATA" },
			{ "BAPI_MDDATASET_GET_AXIS_DATA_SHORT", "RSR_MDX_GET_AXIS_DATA_SHORT" },
			{ "BAPI_MDDATASET_GET_CELL_DATA", "RSR_MDX_GET_CELL_DATA" },
			{ "BAPI_MDDATASET_GET_FLAT_DATA", "RSR_MDX_GET_FLAT_DATA" },
			{ "BAPI_MDDATASET_GET_FS_DATA", "RSR_MDX_GET_FS_DATA" }
		};

		// Token: 0x040000F7 RID: 247
		private static readonly List<string> returnStructureFields = new List<string> { "ID", "NUMBER", "LOG_NO", "MESSAGE_V1", "MESSAGE_V2", "MESSAGE_V3", "MESSAGE_V4", "PARAMETER", "FIELD", "SYSTEM" };

		// Token: 0x040000F8 RID: 248
		private readonly ISapBwProvider provider;

		// Token: 0x040000F9 RID: 249
		private readonly SapBwConnectionStringBuilder connectionStringBuilder;

		// Token: 0x040000FA RID: 250
		private ConnectionState connectionState;

		// Token: 0x040000FB RID: 251
		private IDestination destination;

		// Token: 0x040000FC RID: 252
		private IResource resource;

		// Token: 0x040000FD RID: 253
		private RfcConfigParameters parameters;

		// Token: 0x040000FE RID: 254
		private IFileTracer fileTracer;

		// Token: 0x040000FF RID: 255
		private SapBwModuleHelper helper;

		// Token: 0x04000100 RID: 256
		private SapBwUser sapBwUser;

		// Token: 0x04000101 RID: 257
		private int batchSize;

		// Token: 0x04000102 RID: 258
		private bool stateful;

		// Token: 0x04000103 RID: 259
		private bool traceEnabled;

		// Token: 0x04000104 RID: 260
		private bool verboseEnabled;

		// Token: 0x04000105 RID: 261
		private string serverLevelCacheKeyBase;
	}
}
