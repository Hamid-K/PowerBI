using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x02000013 RID: 19
	public abstract class BaseDataWrapper : IDisposable
	{
		// Token: 0x06000096 RID: 150 RVA: 0x000045B8 File Offset: 0x000027B8
		public override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			if (this.m_underlyingObject == null)
			{
				return ((BaseDataWrapper)obj).m_underlyingObject == null;
			}
			return this.m_underlyingObject.Equals(((BaseDataWrapper)obj).m_underlyingObject);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000460A File Offset: 0x0000280A
		public override int GetHashCode()
		{
			if (this.m_underlyingObject != null)
			{
				return this.m_underlyingObject.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004626 File Offset: 0x00002826
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000462F File Offset: 0x0000282F
		protected BaseDataWrapper(object underlyingObject)
		{
			this.m_underlyingObject = underlyingObject;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004640 File Offset: 0x00002840
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				IDisposable disposable = this.m_underlyingObject as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			this.m_underlyingObject = null;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000466C File Offset: 0x0000286C
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00004674 File Offset: 0x00002874
		protected object UnderlyingObject
		{
			get
			{
				return this.m_underlyingObject;
			}
			set
			{
				RSTrace.DataExtensionTracer.Assert(this.m_underlyingObject == null, "Should never replace the underlying connection");
				this.m_underlyingObject = value;
			}
		}

		// Token: 0x0400007E RID: 126
		private object m_underlyingObject;
	}
}
