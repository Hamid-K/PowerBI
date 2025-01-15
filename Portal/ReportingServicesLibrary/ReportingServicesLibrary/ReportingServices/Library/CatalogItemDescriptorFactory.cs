using System;
using System.Data;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000033 RID: 51
	internal class CatalogItemDescriptorFactory : ICatalogItemDescriptorFactory
	{
		// Token: 0x06000183 RID: 387 RVA: 0x0000D982 File Offset: 0x0000BB82
		public CatalogItemDescriptorFactory(Security secMgr, IPathTranslator pathTranslator, ItemDescriptorOptions options)
		{
			this.m_secMgr = secMgr;
			this.m_pt = pathTranslator;
			this.m_secCache = new SecurityCheckCache(this.m_secMgr, CommonOperation.ReadProperties);
			this.m_options = options;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000D9B4 File Offset: 0x0000BBB4
		public virtual bool BuildFromDbRow(IDataReader reader, out CatalogItemDescriptor itemDescriptor)
		{
			bool flag;
			try
			{
				ItemType @int = (ItemType)reader.GetInt32(DBInterface.FindItemColumns.Type);
				Guid guid = reader.GetGuid(DBInterface.FindItemColumns.PolicyID);
				string text = reader.GetString(DBInterface.FindItemColumns.Path);
				string empty = string.Empty;
				if (!reader.IsDBNull(DBInterface.FindItemColumns.SubType))
				{
					reader.GetString(DBInterface.FindItemColumns.SubType);
				}
				SecurityCheckCache.CheckResult checkResult = this.m_secCache.CheckAccess(@int, guid);
				if (checkResult == SecurityCheckCache.CheckResult.NotChecked)
				{
					byte[] array = null;
					if (!reader.IsDBNull(DBInterface.FindItemColumns.SecurityDescriptor))
					{
						array = DataReaderHelper.ReadAllBytes(reader, DBInterface.FindItemColumns.SecurityDescriptor);
					}
					checkResult = (SystemResourceManager.IsSystemResourcePath(text) ? SecurityCheckCache.CheckResult.AccessGranted : this.m_secCache.CheckAccess(@int, guid, array, this.PathTranslator.CatalogToExternal(new CatalogItemPath(text))));
				}
				if (checkResult != SecurityCheckCache.CheckResult.AccessGranted)
				{
					itemDescriptor = null;
					flag = !this.StopOnNoAccess;
				}
				else
				{
					CatalogItemDescriptor catalogItemDescriptor = new CatalogItemDescriptor();
					string text2 = reader.GetString(DBInterface.FindItemColumns.Name);
					if (text.Length == 0)
					{
						text = CatalogItemNameUtility.PathSeparatorString;
						text2 = CatalogItemNameUtility.PathSeparatorString;
					}
					catalogItemDescriptor.Name = text2;
					catalogItemDescriptor.Path = this.PathTranslator.CatalogToExternal(text);
					catalogItemDescriptor.Type = @int;
					string text3 = this.PathTranslator.PathToExternal(text);
					if (text3 != null)
					{
						catalogItemDescriptor.VirtualPath = text3;
					}
					catalogItemDescriptor.ID = reader.GetGuid(DBInterface.FindItemColumns.ItemID).ToString();
					if (!reader.IsDBNull(DBInterface.FindItemColumns.Size))
					{
						catalogItemDescriptor.Size = DBInterface.ReadInt64AsInt32(reader, DBInterface.FindItemColumns.Size);
					}
					if (!reader.IsDBNull(DBInterface.FindItemColumns.Description))
					{
						catalogItemDescriptor.Description = reader.GetString(DBInterface.FindItemColumns.Description);
					}
					catalogItemDescriptor.CreationDate = reader.GetDateTime(DBInterface.FindItemColumns.CreationDate);
					catalogItemDescriptor.ModifiedDate = reader.GetDateTime(DBInterface.FindItemColumns.ModifiedDate);
					catalogItemDescriptor.CreatedBy = UserUtil.GetUserNameBySid(reader, DBInterface.FindItemColumns.CreatedBySid, DBInterface.FindItemColumns.CreatedByBackup);
					catalogItemDescriptor.ModifiedBy = UserUtil.GetUserNameBySid(reader, DBInterface.FindItemColumns.ModifiedBySid, DBInterface.FindItemColumns.ModifiedByBackup);
					if (!reader.IsDBNull(DBInterface.FindItemColumns.MimeType))
					{
						catalogItemDescriptor.MimeType = reader.GetString(DBInterface.FindItemColumns.MimeType);
					}
					if (!reader.IsDBNull(DBInterface.FindItemColumns.ExecutionDate))
					{
						catalogItemDescriptor.ExecutionDate = reader.GetDateTime(DBInterface.FindItemColumns.ExecutionDate);
					}
					if (!reader.IsDBNull(DBInterface.FindItemColumns.Hidden))
					{
						catalogItemDescriptor.Hidden = reader.GetBoolean(DBInterface.FindItemColumns.Hidden);
					}
					if (!reader.IsDBNull(DBInterface.FindItemColumns.SubType))
					{
						catalogItemDescriptor.SubType = reader.GetString(DBInterface.FindItemColumns.SubType);
					}
					if (!reader.IsDBNull(DBInterface.FindItemColumns.ComponentID))
					{
						catalogItemDescriptor.ComponentID = reader.GetGuid(DBInterface.FindItemColumns.ComponentID);
					}
					itemDescriptor = catalogItemDescriptor;
					flag = true;
				}
			}
			catch (ItemNotFoundException)
			{
				itemDescriptor = null;
				flag = !this.StopOnNoAccess;
			}
			return flag;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000DC64 File Offset: 0x0000BE64
		protected IPathTranslator PathTranslator
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_pt;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		protected bool StopOnNoAccess
		{
			[DebuggerStepThrough]
			get
			{
				return (this.Options & ItemDescriptorOptions.StopOnNoAccess) > ItemDescriptorOptions.None;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000DC79 File Offset: 0x0000BE79
		protected Security SecurityManager
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_secMgr;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000DC81 File Offset: 0x0000BE81
		protected ItemDescriptorOptions Options
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_options;
			}
		}

		// Token: 0x0400011D RID: 285
		private readonly Security m_secMgr;

		// Token: 0x0400011E RID: 286
		private readonly IPathTranslator m_pt;

		// Token: 0x0400011F RID: 287
		private readonly SecurityCheckCache m_secCache;

		// Token: 0x04000120 RID: 288
		private readonly ItemDescriptorOptions m_options;
	}
}
