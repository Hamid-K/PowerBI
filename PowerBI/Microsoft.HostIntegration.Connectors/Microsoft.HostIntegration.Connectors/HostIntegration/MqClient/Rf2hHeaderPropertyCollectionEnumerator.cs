using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B4D RID: 2893
	internal class Rf2hHeaderPropertyCollectionEnumerator
	{
		// Token: 0x06005B6F RID: 23407 RVA: 0x0017859C File Offset: 0x0017679C
		internal Rf2hHeaderPropertyCollectionEnumerator(Rf2hHeaderPropertyCollection parent)
		{
			this.header = parent.header;
			this.folderEnumerator = this.header.Folders.GetEnumerator();
		}

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x06005B70 RID: 23408 RVA: 0x001785C6 File Offset: 0x001767C6
		internal PropertyValueDefinition Current
		{
			get
			{
				return this.propertyEnumerator.Current;
			}
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x001785D3 File Offset: 0x001767D3
		internal bool MoveNext()
		{
			if (this.propertyEnumerator == null && !this.GetToNextPropertiesFolder())
			{
				return false;
			}
			if (!this.propertyEnumerator.MoveNext())
			{
				if (!this.GetToNextPropertiesFolder())
				{
					return false;
				}
				this.propertyEnumerator.MoveNext();
			}
			return true;
		}

		// Token: 0x06005B72 RID: 23410 RVA: 0x0017860C File Offset: 0x0017680C
		private bool GetToNextPropertiesFolder()
		{
			bool flag = true;
			while (flag && this.folderEnumerator.MoveNext())
			{
				Rf2hFolderWithFieldsAndProperties rf2hFolderWithFieldsAndProperties = this.folderEnumerator.Current as Rf2hFolderWithFieldsAndProperties;
				if (rf2hFolderWithFieldsAndProperties != null && rf2hFolderWithFieldsAndProperties.Properties.Count != 0)
				{
					flag = false;
					if (this.propertyEnumerator != null)
					{
						this.propertyEnumerator.Dispose();
					}
					this.propertyEnumerator = rf2hFolderWithFieldsAndProperties.Properties.GetEnumerator();
				}
			}
			return !flag;
		}

		// Token: 0x06005B73 RID: 23411 RVA: 0x00178678 File Offset: 0x00176878
		internal void Dispose()
		{
			if (this.propertyEnumerator != null)
			{
				this.propertyEnumerator.Dispose();
			}
			this.folderEnumerator.Dispose();
		}

		// Token: 0x040047F4 RID: 18420
		private RulesAndFormattingVersion2Header header;

		// Token: 0x040047F5 RID: 18421
		private IEnumerator<Rf2hFolder> folderEnumerator;

		// Token: 0x040047F6 RID: 18422
		private IEnumerator<PropertyValueDefinition> propertyEnumerator;
	}
}
