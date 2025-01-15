using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B51 RID: 2897
	public class MessagePropertyCollectionEnumerator : IEnumerator<PropertyValueDefinition>, IDisposable, IEnumerator
	{
		// Token: 0x06005B97 RID: 23447 RVA: 0x00178C68 File Offset: 0x00176E68
		public MessagePropertyCollectionEnumerator(MessagePropertyCollection parent)
		{
			RulesAndFormattingVersion2Header rf2Header = parent.GetRf2Header(false);
			if (rf2Header != null)
			{
				this.headerPropertyEnumerator = rf2Header.Properties.GetEnumerator();
			}
		}

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06005B98 RID: 23448 RVA: 0x00178C97 File Offset: 0x00176E97
		public PropertyValueDefinition Current
		{
			get
			{
				return this.headerPropertyEnumerator.Current;
			}
		}

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x06005B99 RID: 23449 RVA: 0x00178CA4 File Offset: 0x00176EA4
		private object Current1
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x06005B9A RID: 23450 RVA: 0x00178CAC File Offset: 0x00176EAC
		object IEnumerator.Current
		{
			get
			{
				return this.Current1;
			}
		}

		// Token: 0x06005B9B RID: 23451 RVA: 0x00178CB4 File Offset: 0x00176EB4
		public bool MoveNext()
		{
			return this.headerPropertyEnumerator != null && this.headerPropertyEnumerator.MoveNext();
		}

		// Token: 0x06005B9C RID: 23452 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005B9D RID: 23453 RVA: 0x00178CCB File Offset: 0x00176ECB
		public void Dispose()
		{
			if (this.headerPropertyEnumerator != null)
			{
				this.headerPropertyEnumerator.Dispose();
			}
		}

		// Token: 0x040047FC RID: 18428
		private Rf2hHeaderPropertyCollectionEnumerator headerPropertyEnumerator;
	}
}
