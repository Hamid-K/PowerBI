using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ReportingServices;
using Microsoft.ReportingServices.Portal.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Model
{
	// Token: 0x0200006B RID: 107
	public class DataSource : CatalogItem
	{
		// Token: 0x060002FA RID: 762 RVA: 0x00003A74 File Offset: 0x00001C74
		public DataSource()
			: base(CatalogItemType.DataSource)
		{
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00003A7D File Offset: 0x00001C7D
		// (set) Token: 0x060002FC RID: 764 RVA: 0x00003A85 File Offset: 0x00001C85
		public bool IsEnabled { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00003A8E File Offset: 0x00001C8E
		// (set) Token: 0x060002FE RID: 766 RVA: 0x00003A96 File Offset: 0x00001C96
		public string ConnectionString { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00003A9F File Offset: 0x00001C9F
		// (set) Token: 0x06000300 RID: 768 RVA: 0x00003AA7 File Offset: 0x00001CA7
		public string DataSourceType { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00003AB0 File Offset: 0x00001CB0
		// (set) Token: 0x06000302 RID: 770 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public bool IsOriginalConnectionStringExpressionBased { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00003AC1 File Offset: 0x00001CC1
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00003AC9 File Offset: 0x00001CC9
		public bool IsConnectionStringOverridden { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00003AD2 File Offset: 0x00001CD2
		// (set) Token: 0x06000306 RID: 774 RVA: 0x00003ADA File Offset: 0x00001CDA
		[JsonConverter(typeof(StringEnumConverter))]
		public CredentialRetrievalType CredentialRetrieval { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00003AE3 File Offset: 0x00001CE3
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00003AEB File Offset: 0x00001CEB
		public CredentialsSuppliedByUser CredentialsByUser { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00003AF4 File Offset: 0x00001CF4
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00003AFC File Offset: 0x00001CFC
		public CredentialsStoredInServer CredentialsInServer { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00003B05 File Offset: 0x00001D05
		// (set) Token: 0x0600030C RID: 780 RVA: 0x00003B0D File Offset: 0x00001D0D
		public bool IsReference { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00003B16 File Offset: 0x00001D16
		// (set) Token: 0x0600030E RID: 782 RVA: 0x00003B1E File Offset: 0x00001D1E
		public string DataSourceSubType { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00003B27 File Offset: 0x00001D27
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00003B2F File Offset: 0x00001D2F
		public DataModelDataSource DataModelDataSource { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00003B38 File Offset: 0x00001D38
		public IList<Subscription> Subscriptions
		{
			get
			{
				IList<Subscription> list;
				if ((list = this._subscriptions) == null)
				{
					list = (this._subscriptions = this.LoadSubscriptions());
				}
				return list;
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00003B60 File Offset: 0x00001D60
		public void Validate(IEnumerable<Extension> dataSourceTypes)
		{
			this.ValidateFieldNotNullOrEmpty(this.CredentialRetrieval.ToString(), SR.PARAM_ConnectionType);
			switch (this.CredentialRetrieval)
			{
			case CredentialRetrievalType.prompt:
				this.ValidateFieldNotNull(this.CredentialsByUser, SR.PARAM_UserCredentials);
				this.ValidateFieldNotNull(this.CredentialsByUser.DisplayText, SR.PARAM_UserId);
				break;
			case CredentialRetrievalType.store:
				if (this.CredentialsInServer == null)
				{
					throw new DataValidationException(SR.ERROR_MissingServerCredentials);
				}
				this.ValidateFieldNotNull(this.CredentialsInServer.UserName, SR.PARAM_ServerUserId);
				this.ValidateFieldNotNull(this.CredentialsInServer.Password, SR.PARAM_ServerPassword);
				break;
			}
			this.ValidateFieldNotNullOrEmpty(base.Path, SR.PARAM_Path);
			if (string.IsNullOrEmpty(base.Path) || !base.Path.StartsWith("/"))
			{
				throw new DataValidationException(SR.ERROR_PathStart);
			}
			this.ValidateFieldNotNullOrEmpty(this.DataSourceType, SR.PARAM_DataSourceType);
			if (!dataSourceTypes.Any((Extension s) => s.Name == this.DataSourceType))
			{
				throw new DataValidationException(string.Format(SR.ERROR_BadParameter, SR.PARAM_DataSourceType, this.DataSourceType));
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00003C8E File Offset: 0x00001E8E
		private void ValidateFieldNotNullOrEmpty(string field, string fieldName)
		{
			if (string.IsNullOrEmpty(field))
			{
				throw new DataValidationException(string.Format(SR.ERROR_MissingParameter, fieldName));
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00003CA9 File Offset: 0x00001EA9
		private void ValidateFieldNotNull(object field, string fieldName)
		{
			if (field == null)
			{
				throw new DataValidationException(string.Format(SR.ERROR_MissingParameter, fieldName));
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00003CBF File Offset: 0x00001EBF
		protected virtual IList<Subscription> LoadSubscriptions()
		{
			return new List<Subscription>();
		}

		// Token: 0x0400022F RID: 559
		private IList<Subscription> _subscriptions;
	}
}
