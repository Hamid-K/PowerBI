using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x02000025 RID: 37
	internal sealed class WindowsAce : AceStruct
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00004341 File Offset: 0x00002541
		internal WindowsAce(AceStruct genericAce)
			: base(genericAce)
		{
			this.Operations2Permissions();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004350 File Offset: 0x00002550
		private void Operations2Permissions()
		{
			this.CatPerm = this.ConvertToMask(this.CatalogOperations);
			this.FldPerm = this.ConvertToMask(this.FolderOperations);
			this.RptPermPrim = this.ConvertToMask(this.ReportOperations, SecDescType.ReportPrimary);
			this.RptPermSec = this.ConvertToMask(this.ReportOperations, SecDescType.ReportSecondary);
			this.ResPerm = this.ConvertToMask(this.ResourceOperations);
			this.DSPerm = this.ConvertToMask(this.DatasourceOperations);
			this.ModelPerm = this.ConvertToMask(this.ModelOperations);
			this.ModelItemPerm = this.ConvertToMask(this.ModelItemOperations);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000043F0 File Offset: 0x000025F0
		private uint ConvertToMask(CatalogOperationsCollection operations)
		{
			uint num = 0U;
			for (int i = 0; i < operations.Count; i++)
			{
				CatalogOperation catalogOperation = operations[i];
				num |= (uint)AuthzData.m_CatOper2PermMask[catalogOperation];
			}
			return num;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004434 File Offset: 0x00002634
		private uint ConvertToMask(FolderOperationsCollection operations)
		{
			uint num = 0U;
			for (int i = 0; i < operations.Count; i++)
			{
				FolderOperation folderOperation = operations[i];
				num |= (uint)AuthzData.m_FldOper2PermMask[folderOperation];
			}
			return num;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004478 File Offset: 0x00002678
		private uint ConvertToMask(ReportOperationsCollection operations, SecDescType type)
		{
			uint num = 0U;
			if (type == SecDescType.ReportPrimary)
			{
				for (int i = 0; i < operations.Count; i++)
				{
					ReportOperation reportOperation = operations[i];
					if ((ulong)reportOperation < (ulong)((long)AuthzConstants.MaxAceIndex))
					{
						num |= (uint)AuthzData.m_RptOper2PermMask[reportOperation];
					}
				}
			}
			else
			{
				if (type != SecDescType.ReportSecondary)
				{
					throw new InternalCatalogException("Unexpected security descriptor type");
				}
				for (int j = 0; j < operations.Count; j++)
				{
					ReportOperation reportOperation2 = operations[j];
					if ((ulong)reportOperation2 >= (ulong)((long)AuthzConstants.MaxAceIndex))
					{
						num |= (uint)AuthzData.m_RptOper2PermMask[reportOperation2];
					}
				}
			}
			return num;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004518 File Offset: 0x00002718
		private uint ConvertToMask(ResourceOperationsCollection operations)
		{
			uint num = 0U;
			for (int i = 0; i < operations.Count; i++)
			{
				ResourceOperation resourceOperation = operations[i];
				num |= (uint)AuthzData.m_ResOper2PermMask[resourceOperation];
			}
			return num;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000455C File Offset: 0x0000275C
		private uint ConvertToMask(DatasourceOperationsCollection operations)
		{
			uint num = 0U;
			for (int i = 0; i < operations.Count; i++)
			{
				DatasourceOperation datasourceOperation = operations[i];
				num |= (uint)AuthzData.m_DSOper2PermMask[datasourceOperation];
			}
			return num;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000045A0 File Offset: 0x000027A0
		private uint ConvertToMask(ModelOperationsCollection operations)
		{
			uint num = 0U;
			for (int i = 0; i < operations.Count; i++)
			{
				ModelOperation modelOperation = operations[i];
				num |= (uint)AuthzData.m_ModelOper2PermMask[modelOperation];
			}
			return num;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000045E4 File Offset: 0x000027E4
		private uint ConvertToMask(ModelItemOperationsCollection operations)
		{
			uint num = 0U;
			for (int i = 0; i < operations.Count; i++)
			{
				ModelItemOperation modelItemOperation = operations[i];
				num |= (uint)AuthzData.m_ModelItemOper2PermMask[modelItemOperation];
			}
			return num;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004628 File Offset: 0x00002828
		internal uint GetMask(SecDescType secDescType)
		{
			switch (secDescType)
			{
			case SecDescType.Catalog:
				return this.CatPerm;
			case SecDescType.Folder:
				return this.FldPerm;
			case SecDescType.ReportPrimary:
				return this.RptPermPrim;
			case SecDescType.ReportSecondary:
				return this.RptPermSec;
			case SecDescType.Resource:
				return this.ResPerm;
			case SecDescType.Datasource:
				return this.DSPerm;
			case SecDescType.Model:
				return this.ModelPerm;
			case SecDescType.ModelItem:
				return this.ModelItemPerm;
			default:
				throw new InternalCatalogException("Invalid security descriptor type");
			}
		}

		// Token: 0x04000103 RID: 259
		internal uint CatPerm;

		// Token: 0x04000104 RID: 260
		internal uint RptPermPrim;

		// Token: 0x04000105 RID: 261
		internal uint RptPermSec;

		// Token: 0x04000106 RID: 262
		internal uint FldPerm;

		// Token: 0x04000107 RID: 263
		internal uint ResPerm;

		// Token: 0x04000108 RID: 264
		internal uint DSPerm;

		// Token: 0x04000109 RID: 265
		internal uint ModelPerm;

		// Token: 0x0400010A RID: 266
		internal uint ModelItemPerm;
	}
}
