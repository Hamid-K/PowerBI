using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Translation.DataWrangling
{
	// Token: 0x02001B52 RID: 6994
	public class DataWranglingOperation : IEquatable<DataWranglingOperation>, IEqualityComparer<DataWranglingOperation>
	{
		// Token: 0x0600E59E RID: 58782 RVA: 0x0030A54A File Offset: 0x0030874A
		public DataWranglingOperation(OperationId operationId, IParameters parameters, IEnumerable<string> targets, IEnumerable<string> newColumns)
		{
			this.OperationId = operationId;
			this.Parameters = parameters;
			this.Targets = targets;
			this.NewColumns = newColumns;
		}

		// Token: 0x17002642 RID: 9794
		// (get) Token: 0x0600E59F RID: 58783 RVA: 0x0030A56F File Offset: 0x0030876F
		public OperationId OperationId { get; }

		// Token: 0x17002643 RID: 9795
		// (get) Token: 0x0600E5A0 RID: 58784 RVA: 0x0030A577 File Offset: 0x00308777
		public IParameters Parameters { get; }

		// Token: 0x17002644 RID: 9796
		// (get) Token: 0x0600E5A1 RID: 58785 RVA: 0x0030A57F File Offset: 0x0030877F
		public IEnumerable<string> Targets { get; }

		// Token: 0x17002645 RID: 9797
		// (get) Token: 0x0600E5A2 RID: 58786 RVA: 0x0030A587 File Offset: 0x00308787
		public IEnumerable<string> NewColumns { get; }

		// Token: 0x0600E5A3 RID: 58787 RVA: 0x0030A58F File Offset: 0x0030878F
		public bool Equals(DataWranglingOperation other)
		{
			return other != null && this.ToString() == other.ToString();
		}

		// Token: 0x0600E5A4 RID: 58788 RVA: 0x0030A5A8 File Offset: 0x003087A8
		public bool EqualsIgnoreNewColumns(DataWranglingOperation other)
		{
			return this.OperationId.Equals(other.OperationId) && ((this.Parameters == null && other.Parameters == null) || (this.Parameters != null && this.Parameters.Equals(other.Parameters))) && ((this.Targets == null && other.Targets == null) || (this.Targets != null && this.Targets.SequenceEqual(other.Targets)));
		}

		// Token: 0x0600E5A5 RID: 58789 RVA: 0x0030A630 File Offset: 0x00308830
		public bool Equals(DataWranglingOperation x, DataWranglingOperation y)
		{
			return x.Equals(y);
		}

		// Token: 0x0600E5A6 RID: 58790 RVA: 0x00054234 File Offset: 0x00052434
		public int GetHashCode(DataWranglingOperation obj)
		{
			return obj.GetHashCode();
		}

		// Token: 0x0600E5A7 RID: 58791 RVA: 0x0030A63C File Offset: 0x0030883C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				string.Format("Operation={0};", this.OperationId),
				string.Format("Parameters={0};", (this.Parameters == null) ? "" : this.Parameters),
				"Targets=[",
				(this.Targets == null) ? string.Empty : string.Join(", ", this.Targets),
				"];NewColumns=[",
				(this.NewColumns == null) ? string.Empty : string.Join(", ", this.NewColumns),
				"];"
			});
		}

		// Token: 0x0600E5A8 RID: 58792 RVA: 0x0030A6EA File Offset: 0x003088EA
		public string ToJson()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		// Token: 0x0600E5A9 RID: 58793 RVA: 0x0030A6F4 File Offset: 0x003088F4
		public override bool Equals(object obj)
		{
			DataWranglingOperation dataWranglingOperation = obj as DataWranglingOperation;
			return dataWranglingOperation != null && this.Equals(dataWranglingOperation);
		}

		// Token: 0x0600E5AA RID: 58794 RVA: 0x00218E7F File Offset: 0x0021707F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}
	}
}
