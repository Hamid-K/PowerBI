using System;
using System.Collections.Generic;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x0200000B RID: 11
	internal sealed class FormulaPathPartitionKey : IMemberLetPartitionKey, IPartitionKey, IEquatable<IPartitionKey>
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002DC7 File Offset: 0x00000FC7
		public FormulaPathPartitionKey(string sectionName, string formulaName)
			: this(sectionName, formulaName, null)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002DD2 File Offset: 0x00000FD2
		public FormulaPathPartitionKey(string sectionName, string formulaName, string formulaPartName)
		{
			if (sectionName == null || formulaName == null)
			{
				throw new InvalidOperationException();
			}
			this.sectionName = sectionName;
			this.formulaName = formulaName;
			this.formulaPartName = formulaPartName;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002DFB File Offset: 0x00000FFB
		public PartitioningScheme PartitioningScheme
		{
			get
			{
				return PartitioningScheme.MemberLet1;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002DFE File Offset: 0x00000FFE
		public string Section
		{
			get
			{
				return this.sectionName;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002E06 File Offset: 0x00001006
		public string Member
		{
			get
			{
				return this.formulaName;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002E0E File Offset: 0x0000100E
		public IList<string> Lets
		{
			get
			{
				if (this.formulaPartName == null)
				{
					return new string[0];
				}
				return new string[] { this.formulaPartName };
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002E2E File Offset: 0x0000102E
		public bool Equals(IPartitionKey partitionKey)
		{
			return PartitionKeyEqualityComparer.Instance.Equals(this, partitionKey);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E3C File Offset: 0x0000103C
		public override string ToString()
		{
			return this.ToSerializedString();
		}

		// Token: 0x0400001B RID: 27
		private readonly string sectionName;

		// Token: 0x0400001C RID: 28
		private readonly string formulaName;

		// Token: 0x0400001D RID: 29
		private readonly string formulaPartName;
	}
}
