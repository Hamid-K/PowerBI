using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x02001903 RID: 6403
	internal sealed class SapBwOlapDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A338 RID: 41784 RVA: 0x0021CC4A File Offset: 0x0021AE4A
		public SapBwOlapDataSourceLocation()
		{
			base.Protocol = "sap-bw-olap";
		}

		// Token: 0x170029BE RID: 10686
		// (get) Token: 0x0600A339 RID: 41785 RVA: 0x0021CC5D File Offset: 0x0021AE5D
		public override IEnumerable<string> DisplayAddressFields
		{
			get
			{
				return SapBwOlapDataSourceLocation.displayAddressFields;
			}
		}

		// Token: 0x170029BF RID: 10687
		// (get) Token: 0x0600A33A RID: 41786 RVA: 0x0021CC64 File Offset: 0x0021AE64
		public override string FriendlyName
		{
			get
			{
				IResource resource;
				if (this.TryGetResource(out resource))
				{
					return resource.Path;
				}
				return base.FriendlyName;
			}
		}

		// Token: 0x170029C0 RID: 10688
		// (get) Token: 0x0600A33B RID: 41787 RVA: 0x000700A8 File Offset: 0x0006E2A8
		public override string ResourceKind
		{
			get
			{
				return "SapBusinessWarehouse";
			}
		}

		// Token: 0x170029C1 RID: 10689
		// (get) Token: 0x0600A33C RID: 41788 RVA: 0x0021922B File Offset: 0x0021742B
		// (set) Token: 0x0600A33D RID: 41789 RVA: 0x0021CC88 File Offset: 0x0021AE88
		public string Server
		{
			get
			{
				return base.Address.GetStringOrNull("server");
			}
			set
			{
				base.Address["server"] = value;
				this.connectionType = null;
			}
		}

		// Token: 0x170029C2 RID: 10690
		// (get) Token: 0x0600A33E RID: 41790 RVA: 0x0021CCA7 File Offset: 0x0021AEA7
		// (set) Token: 0x0600A33F RID: 41791 RVA: 0x0021CCB9 File Offset: 0x0021AEB9
		public string SystemNumber
		{
			get
			{
				return base.Address.GetStringOrNull("systemNumber");
			}
			set
			{
				base.Address["systemNumber"] = value;
				this.connectionType = null;
			}
		}

		// Token: 0x170029C3 RID: 10691
		// (get) Token: 0x0600A340 RID: 41792 RVA: 0x0021CCD8 File Offset: 0x0021AED8
		// (set) Token: 0x0600A341 RID: 41793 RVA: 0x0021CCEA File Offset: 0x0021AEEA
		public string ClientId
		{
			get
			{
				return base.Address.GetStringOrNull("clientId");
			}
			set
			{
				base.Address["clientId"] = value;
				this.connectionType = null;
			}
		}

		// Token: 0x170029C4 RID: 10692
		// (get) Token: 0x0600A342 RID: 41794 RVA: 0x0021CD09 File Offset: 0x0021AF09
		// (set) Token: 0x0600A343 RID: 41795 RVA: 0x0021CD1B File Offset: 0x0021AF1B
		public string SystemId
		{
			get
			{
				return base.Address.GetStringOrNull("systemId");
			}
			set
			{
				base.Address["systemId"] = value;
				this.connectionType = null;
			}
		}

		// Token: 0x170029C5 RID: 10693
		// (get) Token: 0x0600A344 RID: 41796 RVA: 0x0021CD3A File Offset: 0x0021AF3A
		// (set) Token: 0x0600A345 RID: 41797 RVA: 0x0021CD4C File Offset: 0x0021AF4C
		public string LogonGroup
		{
			get
			{
				return base.Address.GetStringOrNull("logonGroup");
			}
			set
			{
				base.Address["logonGroup"] = value;
				this.connectionType = null;
			}
		}

		// Token: 0x170029C6 RID: 10694
		// (get) Token: 0x0600A346 RID: 41798 RVA: 0x00219250 File Offset: 0x00217450
		// (set) Token: 0x0600A347 RID: 41799 RVA: 0x00219262 File Offset: 0x00217462
		public string Database
		{
			get
			{
				return base.Address.GetStringOrNull("database");
			}
			set
			{
				base.Address["database"] = value;
			}
		}

		// Token: 0x170029C7 RID: 10695
		// (get) Token: 0x0600A348 RID: 41800 RVA: 0x0021CD6B File Offset: 0x0021AF6B
		// (set) Token: 0x0600A349 RID: 41801 RVA: 0x0021CD7D File Offset: 0x0021AF7D
		public string Cube
		{
			get
			{
				return base.Address.GetStringOrNull("cube");
			}
			set
			{
				base.Address["cube"] = value;
			}
		}

		// Token: 0x170029C8 RID: 10696
		// (get) Token: 0x0600A34A RID: 41802 RVA: 0x0021CD90 File Offset: 0x0021AF90
		private SapBwConnectionType ConnectionType
		{
			get
			{
				if (this.connectionType == null)
				{
					if (SapBwOlapDataSourceLocation.IsValidGroup(this.LogonGroup) && SapBwOlapDataSourceLocation.IsValidSystemId(this.SystemId))
					{
						this.connectionType = new SapBwConnectionType?(SapBwConnectionType.LoadBalanced);
					}
					else if (SapBwOlapDataSourceLocation.IsValidSystemNumber(this.SystemNumber))
					{
						this.connectionType = new SapBwConnectionType?(SapBwConnectionType.ApplicationHostBased);
					}
					SapBwRouterString sapBwRouterString;
					if (this.connectionType == null || !SapBwOlapDataSourceLocation.IsValidServerOrRouterString(this.connectionType.Value, this.Server, this.LogonGroup, false, out sapBwRouterString) || !SapBwOlapDataSourceLocation.IsValidClientId(this.ClientId))
					{
						this.connectionType = new SapBwConnectionType?(SapBwConnectionType.Invalid);
					}
				}
				return this.connectionType.Value;
			}
		}

		// Token: 0x0600A34B RID: 41803 RVA: 0x0021CE40 File Offset: 0x0021B040
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue;
			try
			{
				recordValue = SapBusinessWarehouseModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			if (this.ConnectionType != SapBwConnectionType.Invalid)
			{
				if (this.Database == null && this.Cube == null && base.Query == null)
				{
					return this.GetInvocation(recordValue);
				}
				if (SapBwOlapDataSourceLocation.IsValidDatabase(this.Database) && this.Cube == null && base.Query == null)
				{
					ExpressionBuilder instance = ExpressionBuilder.Instance;
					return new FormulaCreationResult(instance.Let(new VariableInitializer[]
					{
						instance.Declare("Source", this.GetSourceExpression(recordValue), true),
						instance.Declare("Database", instance.Navigate(instance.Identifier("Source"), "Name", this.Database, "Data"), true)
					}));
				}
				if (SapBwOlapDataSourceLocation.IsValidDatabase(this.Database) && SapBwOlapDataSourceLocation.IsValidCube(this.Cube) && base.Query == null)
				{
					ExpressionBuilder instance2 = ExpressionBuilder.Instance;
					return new FormulaCreationResult(instance2.Let(new VariableInitializer[]
					{
						instance2.Declare("Source", this.GetSourceExpression(recordValue), true),
						instance2.Declare("Database", instance2.Navigate(instance2.Identifier("Source"), "Name", this.Database, "Data"), true),
						instance2.Declare("Cube", instance2.Navigate(instance2.Identifier("Database"), "Id", this.Cube, "Data"), true)
					}));
				}
				if (this.Cube == null && SapBwOlapDataSourceLocation.IsValidQuery(base.Query))
				{
					recordValue = recordValue ?? RecordValue.Empty;
					recordValue = recordValue.Concatenate(RecordValue.New(Keys.New("Query"), new Value[] { TextValue.New(base.Query) })).AsRecord;
					return this.GetInvocation(recordValue);
				}
			}
			return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
		}

		// Token: 0x0600A34C RID: 41804 RVA: 0x0021D05C File Offset: 0x0021B25C
		private IFormulaCreationResult GetInvocation(RecordValue optionsRecord)
		{
			return new FormulaCreationResult(this.GetSourceExpression(optionsRecord));
		}

		// Token: 0x0600A34D RID: 41805 RVA: 0x0021D06C File Offset: 0x0021B26C
		private IExpression GetSourceExpression(RecordValue optionsRecord)
		{
			if (this.ConnectionType == SapBwConnectionType.LoadBalanced)
			{
				return ExpressionBuilder.Instance.Invoke("SapBusinessWarehouse.Cubes", 4, new object[] { this.Server, this.SystemId, this.ClientId, this.LogonGroup, optionsRecord });
			}
			return ExpressionBuilder.Instance.Invoke("SapBusinessWarehouse.Cubes", 3, new object[] { this.Server, this.SystemNumber, this.ClientId, optionsRecord });
		}

		// Token: 0x0600A34E RID: 41806 RVA: 0x0021D0F8 File Offset: 0x0021B2F8
		public override bool TryGetResource(out IResource resource)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			if (this.ConnectionType != SapBwConnectionType.Invalid)
			{
				text3 = (SapBwRouterString.IsRouterString(this.Server) ? this.Server.Replace('/', '|') : this.Server);
			}
			switch (this.ConnectionType)
			{
			case SapBwConnectionType.ApplicationHostBased:
				text = string.Join("/", new string[] { text3, this.SystemNumber, this.ClientId });
				text2 = string.Join("/", new string[]
				{
					text3.ToLowerInvariant(),
					this.SystemNumber,
					this.ClientId
				});
				break;
			case SapBwConnectionType.LoadBalanced:
				text = string.Join("/", new string[] { text3, this.SystemId, this.ClientId, this.LogonGroup });
				text2 = string.Join("/", new string[]
				{
					text3.ToLowerInvariant(),
					this.SystemId,
					this.ClientId,
					this.LogonGroup
				});
				break;
			case SapBwConnectionType.Invalid:
				resource = null;
				return false;
			}
			resource = new Resource("SapBusinessWarehouse", text2, text);
			return true;
		}

		// Token: 0x0600A34F RID: 41807 RVA: 0x00219500 File Offset: 0x00217700
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolveServer(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A350 RID: 41808 RVA: 0x0021D228 File Offset: 0x0021B428
		public static bool TryCreateFromResourcePath(string resourcePath, out SapBwOlapDataSourceLocation location)
		{
			string[] array = resourcePath.Split(new char[] { "/"[0] });
			string text = null;
			if (array.Length >= 3 && !SapBwRouterString.TryExtractServerFromResourcePath(array, out text))
			{
				location = null;
				return false;
			}
			if (array.Length == 3)
			{
				location = new SapBwOlapDataSourceLocation
				{
					Server = text,
					SystemNumber = array[1],
					ClientId = array[2]
				};
				return true;
			}
			if (array.Length == 4)
			{
				location = new SapBwOlapDataSourceLocation
				{
					Server = text,
					SystemId = array[1],
					ClientId = array[2],
					LogonGroup = array[3]
				};
				return true;
			}
			location = null;
			return false;
		}

		// Token: 0x0600A351 RID: 41809 RVA: 0x0021D2C4 File Offset: 0x0021B4C4
		public static bool IsValidSystemNumber(string systemNumber)
		{
			return systemNumber != null && SapBwOlapDataSourceLocation.systemNumberRegex.IsMatch(systemNumber);
		}

		// Token: 0x0600A352 RID: 41810 RVA: 0x0021D2D6 File Offset: 0x0021B4D6
		public static bool IsValidClientId(string clientId)
		{
			return clientId != null && SapBwOlapDataSourceLocation.clientIdRegex.IsMatch(clientId);
		}

		// Token: 0x0600A353 RID: 41811 RVA: 0x0021D2E8 File Offset: 0x0021B4E8
		public static bool IsValidServerOrRouterString(SapBwConnectionType connectionType, string serverName, string logonGroup, bool throwOnError, out SapBwRouterString routerString)
		{
			string text = null;
			if (serverName != null)
			{
				if (Uri.CheckHostName(serverName) != UriHostNameType.Unknown)
				{
					routerString = null;
					return true;
				}
				try
				{
					if (SapBwRouterString.TryNew(connectionType, serverName, logonGroup, out routerString))
					{
						return true;
					}
				}
				catch (FormatException ex)
				{
					text = ex.Message;
				}
			}
			if (throwOnError)
			{
				throw ValueException.NewExpressionError(text ?? Strings.SapBwInvalidServerName, TextValue.New(serverName), null);
			}
			routerString = null;
			return false;
		}

		// Token: 0x0600A354 RID: 41812 RVA: 0x0021D35C File Offset: 0x0021B55C
		public static bool IsValidDatabase(string databaseName)
		{
			return !string.IsNullOrEmpty(databaseName);
		}

		// Token: 0x0600A355 RID: 41813 RVA: 0x0021D35C File Offset: 0x0021B55C
		public static bool IsValidCube(string cubeName)
		{
			return !string.IsNullOrEmpty(cubeName);
		}

		// Token: 0x0600A356 RID: 41814 RVA: 0x0021D35C File Offset: 0x0021B55C
		public static bool IsValidQuery(string query)
		{
			return !string.IsNullOrEmpty(query);
		}

		// Token: 0x0600A357 RID: 41815 RVA: 0x0021D35C File Offset: 0x0021B55C
		public static bool IsValidSystemId(string systemId)
		{
			return !string.IsNullOrEmpty(systemId);
		}

		// Token: 0x0600A358 RID: 41816 RVA: 0x0021D35C File Offset: 0x0021B55C
		public static bool IsValidGroup(string group)
		{
			return !string.IsNullOrEmpty(group);
		}

		// Token: 0x040054F4 RID: 21748
		public static readonly DataSourceLocationFactory Factory = new SapBwOlapDataSourceLocation.DslFactory();

		// Token: 0x040054F5 RID: 21749
		public const string DatabaseFieldName = "database";

		// Token: 0x040054F6 RID: 21750
		public const string ClientIdFieldName = "clientId";

		// Token: 0x040054F7 RID: 21751
		public const string SystemNumberFieldName = "systemNumber";

		// Token: 0x040054F8 RID: 21752
		public const string SystemIdFieldName = "systemId";

		// Token: 0x040054F9 RID: 21753
		public const string LogonGroupFieldName = "logonGroup";

		// Token: 0x040054FA RID: 21754
		public const string CubeFieldName = "cube";

		// Token: 0x040054FB RID: 21755
		private static readonly Regex systemNumberRegex = new Regex("^[0-9]{2}\\z");

		// Token: 0x040054FC RID: 21756
		private static readonly Regex clientIdRegex = new Regex("^[0-9]{3}\\z");

		// Token: 0x040054FD RID: 21757
		private static readonly string[] displayAddressFields = new string[] { "server", "systemNumber", "systemId", "clientId", "logonGroup" };

		// Token: 0x040054FE RID: 21758
		private const string resourcePathSeparator = "/";

		// Token: 0x040054FF RID: 21759
		private SapBwConnectionType? connectionType;

		// Token: 0x02001904 RID: 6404
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029C9 RID: 10697
			// (get) Token: 0x0600A35A RID: 41818 RVA: 0x0021D3D0 File Offset: 0x0021B5D0
			public override string Protocol
			{
				get
				{
					return "sap-bw-olap";
				}
			}

			// Token: 0x0600A35B RID: 41819 RVA: 0x0021D3D7 File Offset: 0x0021B5D7
			public override IDataSourceLocation New()
			{
				return new SapBwOlapDataSourceLocation();
			}

			// Token: 0x0600A35C RID: 41820 RVA: 0x0021D3E0 File Offset: 0x0021B5E0
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				SapBwOlapDataSourceLocation sapBwOlapDataSourceLocation;
				if (SapBwOlapDataSourceLocation.TryCreateFromResourcePath(resourcePath, out sapBwOlapDataSourceLocation))
				{
					location = sapBwOlapDataSourceLocation;
					return true;
				}
				return base.TryCreateFromResourcePath(resourcePath, out location);
			}
		}
	}
}
