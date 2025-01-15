using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B4F RID: 2895
	public class Rf2hFolderPropertyCollectionEnumerator : IEnumerator<PropertyValueDefinition>, IDisposable, IEnumerator
	{
		// Token: 0x06005B84 RID: 23428 RVA: 0x00178A14 File Offset: 0x00176C14
		internal Rf2hFolderPropertyCollectionEnumerator(Rf2hFolderPropertyCollection parent)
		{
			this.parent = parent;
			this.dictionaryEnumerator = parent.NamesToProperties.GetEnumerator();
		}

		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x06005B85 RID: 23429 RVA: 0x00178A34 File Offset: 0x00176C34
		public PropertyValueDefinition Current
		{
			get
			{
				KeyValuePair<string, PropertyValueDefinition> keyValuePair = this.dictionaryEnumerator.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x06005B86 RID: 23430 RVA: 0x00178A54 File Offset: 0x00176C54
		private object Current1
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06005B87 RID: 23431 RVA: 0x00178A5C File Offset: 0x00176C5C
		object IEnumerator.Current
		{
			get
			{
				return this.Current1;
			}
		}

		// Token: 0x06005B88 RID: 23432 RVA: 0x00178A64 File Offset: 0x00176C64
		public bool MoveNext()
		{
			return this.dictionaryEnumerator.MoveNext();
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x00178A71 File Offset: 0x00176C71
		public void Dispose()
		{
			this.dictionaryEnumerator.Dispose();
		}

		// Token: 0x040047F9 RID: 18425
		private Rf2hFolderPropertyCollection parent;

		// Token: 0x040047FA RID: 18426
		private Dictionary<string, PropertyValueDefinition>.Enumerator dictionaryEnumerator;
	}
}
