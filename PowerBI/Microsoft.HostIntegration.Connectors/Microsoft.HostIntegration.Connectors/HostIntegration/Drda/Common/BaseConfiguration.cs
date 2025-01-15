using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000793 RID: 1939
	public class BaseConfiguration : IConfiguration
	{
		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06003E85 RID: 16005 RVA: 0x000D1C1B File Offset: 0x000CFE1B
		public string ServiceName
		{
			get
			{
				return this._serviceName;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06003E86 RID: 16006 RVA: 0x000D1C23 File Offset: 0x000CFE23
		public Dictionary<ManagerCodePoint, ManagerElement> Managers
		{
			get
			{
				return this._managers;
			}
		}

		// Token: 0x17000EC7 RID: 3783
		public ManagerElement this[ManagerCodePoint cp]
		{
			get
			{
				return this._managers[cp];
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06003E88 RID: 16008 RVA: 0x000D1C39 File Offset: 0x000CFE39
		public List<TypeElement> CommunicationManagers
		{
			get
			{
				return this._commManagers;
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06003E89 RID: 16009 RVA: 0x000D1C41 File Offset: 0x000CFE41
		public TypeElement ExceptionManager
		{
			get
			{
				return this._exManager;
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x000D1C49 File Offset: 0x000CFE49
		public List<TypeElement> CustomLoggers
		{
			get
			{
				return this._customLoggers;
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06003E8B RID: 16011 RVA: 0x000D1C51 File Offset: 0x000CFE51
		public List<TypeElement> PackageBindListeners
		{
			get
			{
				return this._packageBindListners;
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06003E8C RID: 16012 RVA: 0x000D1C59 File Offset: 0x000CFE59
		public TypeElement Database
		{
			get
			{
				return this._database;
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06003E8D RID: 16013 RVA: 0x000D1C61 File Offset: 0x000CFE61
		public List<ApplicationEncoding> ApplicationEncodings
		{
			get
			{
				return this._applicationEncodings;
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06003E8E RID: 16014 RVA: 0x000D1C69 File Offset: 0x000CFE69
		public List<DateTimeMask> DateTimeMasks
		{
			get
			{
				return this._dateTimeMasks;
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06003E8F RID: 16015 RVA: 0x000D1C71 File Offset: 0x000CFE71
		public Dictionary<SqlSetOptions, string> SqlSets
		{
			get
			{
				return this._sqlSetOptions;
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06003E90 RID: 16016 RVA: 0x000D1C79 File Offset: 0x000CFE79
		public DatabaseAlias DatabaseAliases
		{
			get
			{
				return this._databaseAliases;
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06003E91 RID: 16017 RVA: 0x000D1C81 File Offset: 0x000CFE81
		public Dictionary<string, string> CollationMappings
		{
			get
			{
				return this._collationMappings;
			}
		}

		// Token: 0x04002519 RID: 9497
		protected DatabaseAlias _databaseAliases = new DatabaseAlias();

		// Token: 0x0400251A RID: 9498
		private Dictionary<string, string> _collationMappings = new Dictionary<string, string>();

		// Token: 0x0400251B RID: 9499
		protected List<DateTimeMask> _dateTimeMasks = new List<DateTimeMask>();

		// Token: 0x0400251C RID: 9500
		protected Dictionary<SqlSetOptions, string> _sqlSetOptions = new Dictionary<SqlSetOptions, string>();

		// Token: 0x0400251D RID: 9501
		protected Dictionary<ManagerCodePoint, ManagerElement> _managers = new Dictionary<ManagerCodePoint, ManagerElement>();

		// Token: 0x0400251E RID: 9502
		protected TypeElement _exManager = new TypeElement();

		// Token: 0x0400251F RID: 9503
		protected TypeElement _database = new TypeElement();

		// Token: 0x04002520 RID: 9504
		protected List<TypeElement> _customLoggers = new List<TypeElement>();

		// Token: 0x04002521 RID: 9505
		protected List<TypeElement> _packageBindListners = new List<TypeElement>();

		// Token: 0x04002522 RID: 9506
		protected List<TypeElement> _commManagers = new List<TypeElement>();

		// Token: 0x04002523 RID: 9507
		protected List<ApplicationEncoding> _applicationEncodings = new List<ApplicationEncoding>();

		// Token: 0x04002524 RID: 9508
		protected string _serviceName = "DrdaService1";
	}
}
