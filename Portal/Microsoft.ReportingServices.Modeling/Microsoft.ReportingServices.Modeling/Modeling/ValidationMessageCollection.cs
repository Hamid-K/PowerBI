using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000C6 RID: 198
	public sealed class ValidationMessageCollection : ReadOnlyCollection<ValidationMessage>
	{
		// Token: 0x06000B4A RID: 2890 RVA: 0x00025288 File Offset: 0x00023488
		public ValidationMessageCollection(IList<ValidationMessage> list)
			: base(list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].Severity == Severity.Error)
				{
					this.m_firstErrorIndex = i;
					return;
				}
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000252CA File Offset: 0x000234CA
		public ValidationMessageCollection(ValidationMessage message)
			: this(new ValidationMessage[] { message })
		{
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000252DC File Offset: 0x000234DC
		internal ValidationMessageCollection(IList<ValidationMessage> list, int firstErrorIndex)
			: base(list)
		{
			if (firstErrorIndex < -1)
			{
				throw new InternalModelingException("firstErrorIndex less than -1");
			}
			this.m_firstErrorIndex = firstErrorIndex;
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00025302 File Offset: 0x00023502
		public bool HasErrors
		{
			get
			{
				return this.m_firstErrorIndex >= 0;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00025310 File Offset: 0x00023510
		public ValidationMessage FirstError
		{
			get
			{
				if (this.m_firstErrorIndex < 0)
				{
					return null;
				}
				return base[this.m_firstErrorIndex];
			}
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00025329 File Offset: 0x00023529
		public IEnumerable<ValidationMessage> GetErrors()
		{
			foreach (ValidationMessage validationMessage in this)
			{
				if (validationMessage.Severity == Severity.Error)
				{
					yield return validationMessage;
				}
			}
			IEnumerator<ValidationMessage> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00025339 File Offset: 0x00023539
		public IEnumerable<ValidationMessage> GetWarnings()
		{
			foreach (ValidationMessage validationMessage in this)
			{
				if (validationMessage.Severity == Severity.Warning)
				{
					yield return validationMessage;
				}
			}
			IEnumerator<ValidationMessage> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040004A5 RID: 1189
		private readonly int m_firstErrorIndex = -1;
	}
}
