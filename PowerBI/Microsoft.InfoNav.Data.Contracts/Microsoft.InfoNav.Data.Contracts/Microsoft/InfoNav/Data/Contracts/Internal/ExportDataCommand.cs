using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001CE RID: 462
	[DataContract(Name = "ExportDataCommand", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ExportDataCommand : IEquatable<ExportDataCommand>
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x000180B8 File Offset: 0x000162B8
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x000180C0 File Offset: 0x000162C0
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public IList<ExportDataColumn> Columns { get; set; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x000180C9 File Offset: 0x000162C9
		// (set) Token: 0x06000C3E RID: 3134 RVA: 0x000180D1 File Offset: 0x000162D1
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 20)]
		public IList<int> Ordering { get; set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x000180DA File Offset: 0x000162DA
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x000180E2 File Offset: 0x000162E2
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 30)]
		public string FiltersDescription { get; set; }

		// Token: 0x06000C41 RID: 3137 RVA: 0x000180EC File Offset: 0x000162EC
		public static bool TryValidate(ExportDataCommand exportDataCommand, out IEnumerable<ValidationResult> validationResults)
		{
			validationResults = null;
			if (exportDataCommand == null)
			{
				validationResults = ExportDataCommand.CreateValidationResult("There is no ExportDataCommand");
			}
			else if (exportDataCommand.Columns == null || exportDataCommand.Columns.Count == 0)
			{
				validationResults = ExportDataCommand.CreateValidationResult("Invalid Columns");
			}
			else if (exportDataCommand.FiltersDescription == null)
			{
				validationResults = ExportDataCommand.CreateValidationResult("There is no FiltersDescription");
			}
			return validationResults == null;
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0001814F File Offset: 0x0001634F
		private static IEnumerable<ValidationResult> CreateValidationResult(string errorMessage)
		{
			return new ValidationResult[]
			{
				new ValidationResult(errorMessage)
			};
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00018160 File Offset: 0x00016360
		public bool Equals(ExportDataCommand other)
		{
			bool? flag = Util.AreEqual<ExportDataCommand>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			flag = Util.AreEqual<IList<ExportDataColumn>>(this.Columns, other.Columns);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Columns.SequenceEqual(other.Columns) && this.Ordering.SequenceEqual(other.Ordering) && this.FiltersDescription.Equals(other.FiltersDescription);
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x000181E2 File Offset: 0x000163E2
		public override bool Equals(object other)
		{
			return this.Equals(other as ExportDataCommand);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000181F0 File Offset: 0x000163F0
		public override int GetHashCode()
		{
			if (this.Columns == null)
			{
				return 0;
			}
			return Hashing.CombineHash<ExportDataColumn>(this.Columns, null);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00018208 File Offset: 0x00016408
		public static bool operator ==(ExportDataCommand left, ExportDataCommand right)
		{
			bool? flag = Util.AreEqual<ExportDataCommand>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00018235 File Offset: 0x00016435
		public static bool operator !=(ExportDataCommand left, ExportDataCommand right)
		{
			return !(left == right);
		}
	}
}
