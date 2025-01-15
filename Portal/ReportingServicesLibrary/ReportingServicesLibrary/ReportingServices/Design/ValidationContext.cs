using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Design
{
	// Token: 0x0200038D RID: 909
	public class ValidationContext
	{
		// Token: 0x06001E09 RID: 7689 RVA: 0x0007AEA0 File Offset: 0x000790A0
		public IList<ValidationResult> GetAllResults()
		{
			return this.m_results.AsReadOnly();
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x0007AEAD File Offset: 0x000790AD
		public bool HasResults
		{
			get
			{
				return this.m_results.Count > 0;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x0007AEBD File Offset: 0x000790BD
		public ValidationResult FirstResult
		{
			get
			{
				if (!this.HasResults)
				{
					return null;
				}
				return this.m_results[0];
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0007AED5 File Offset: 0x000790D5
		public bool HasErrors
		{
			get
			{
				return this.m_firstErrorIndex != -1;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x0007AEE3 File Offset: 0x000790E3
		public ValidationResult FirstError
		{
			get
			{
				if (this.m_firstErrorIndex != -1)
				{
					return this.m_results[this.m_firstErrorIndex];
				}
				return null;
			}
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x0007AF01 File Offset: 0x00079101
		public void AddResult(ValidationResult r)
		{
			if (this.m_firstErrorIndex == -1 && r.IsError)
			{
				this.m_firstErrorIndex = this.m_results.Count;
			}
			this.m_results.Add(r);
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x0007AF31 File Offset: 0x00079131
		public void AddGeneralError(object obj, string message)
		{
			if (this.m_firstErrorIndex == -1)
			{
				this.m_firstErrorIndex = this.m_results.Count;
			}
			this.m_results.Add(new GeneralValidationResult(obj, true, message));
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x0007AF60 File Offset: 0x00079160
		public void AddGeneralWarning(object obj, string message)
		{
			this.m_results.Add(new GeneralValidationResult(obj, false, message));
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x0007AF78 File Offset: 0x00079178
		public void ConsolidateResults(IList<ValidationResult> oldResults, ValidationResult newResult)
		{
			if (oldResults == null)
			{
				throw new ArgumentNullException("oldResults");
			}
			if (newResult == null)
			{
				throw new ArgumentNullException("newResult");
			}
			if (oldResults.Count == 0)
			{
				throw new ArgumentException("oldResults");
			}
			Bag<ValidationResult> bag = new Bag<ValidationResult>();
			bool flag = false;
			int num = -1;
			for (int i = this.m_results.Count - 1; i >= 0; i--)
			{
				ValidationResult validationResult = this.m_results[i];
				if (oldResults.Contains(validationResult))
				{
					flag = flag || validationResult.IsError;
					bag.Add(validationResult);
					this.m_results.RemoveAt(i);
					num = i;
				}
			}
			if (bag.Count != oldResults.Count)
			{
				throw new ArgumentException("Specified old result was not found");
			}
			if (num == -1)
			{
				throw new ApplicationException("Insert index not found");
			}
			if (flag && !newResult.IsError)
			{
				throw new ArgumentException("Cannot replace error results with non-errors");
			}
			this.m_results.Insert(num, newResult);
			if (this.m_firstErrorIndex >= num)
			{
				this.UpdateFirstErrorIndex();
			}
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x0007B06C File Offset: 0x0007926C
		private void UpdateFirstErrorIndex()
		{
			this.m_firstErrorIndex = -1;
			for (int i = 0; i < this.m_results.Count; i++)
			{
				if (this.m_results[i].IsError)
				{
					this.m_firstErrorIndex = i;
					return;
				}
			}
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x0007B0B4 File Offset: 0x000792B4
		public string GetResultSummary()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ValidationResult validationResult in this.m_results)
			{
				stringBuilder.AppendLine(validationResult.ToString());
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000CBD RID: 3261
		private List<ValidationResult> m_results = new List<ValidationResult>();

		// Token: 0x04000CBE RID: 3262
		private int m_firstErrorIndex = -1;
	}
}
