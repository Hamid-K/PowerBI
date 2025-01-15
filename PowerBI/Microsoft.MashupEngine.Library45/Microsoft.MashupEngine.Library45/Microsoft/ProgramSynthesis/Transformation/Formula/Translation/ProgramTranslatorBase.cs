using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x0200180B RID: 6155
	internal abstract class ProgramTranslatorBase
	{
		// Token: 0x0600CA40 RID: 51776 RVA: 0x002B3A2C File Offset: 0x002B1C2C
		internal ProgramTranslatorBase(Program program, IEnumerable<Example> examples, IEnumerable<IRow> inputs, ILogger logger = null)
		{
			this.Examples = examples.ToReadOnlyList<Example>();
			this.Inputs = inputs.ToReadOnlyList<IRow>();
			this.Program = program;
			this.Logger = logger;
		}

		// Token: 0x1700221B RID: 8731
		// (get) Token: 0x0600CA41 RID: 51777 RVA: 0x002B3A68 File Offset: 0x002B1C68
		public IReadOnlyList<IRow> AllInputs
		{
			get
			{
				IReadOnlyList<IRow> readOnlyList;
				if ((readOnlyList = this._allInputs) == null)
				{
					readOnlyList = (this._allInputs = this.Examples.Select((Example e) => e.Input).Concat(this.Inputs).ToReadOnlyList<IRow>());
				}
				return readOnlyList;
			}
		}

		// Token: 0x1700221C RID: 8732
		// (get) Token: 0x0600CA42 RID: 51778 RVA: 0x002B3AC2 File Offset: 0x002B1CC2
		public IReadOnlyList<Example> Examples { get; }

		// Token: 0x1700221D RID: 8733
		// (get) Token: 0x0600CA43 RID: 51779 RVA: 0x002B3ACA File Offset: 0x002B1CCA
		public IReadOnlyList<IRow> Inputs { get; }

		// Token: 0x1700221E RID: 8734
		// (get) Token: 0x0600CA44 RID: 51780 RVA: 0x002B3AD2 File Offset: 0x002B1CD2
		public Program Program { get; }

		// Token: 0x1700221F RID: 8735
		// (get) Token: 0x0600CA45 RID: 51781 RVA: 0x002B3ADC File Offset: 0x002B1CDC
		protected IReadOnlyList<ColumnDetail> InputColumnDetails
		{
			get
			{
				List<ColumnDetail> list;
				if ((list = this._inputColumnDetails) == null)
				{
					list = (this._inputColumnDetails = (from d in this.Examples.Select((Example e) => e.Input).Concat(this.Inputs).InputColumnDetails(null)
						orderby d.Name
						select d).ToList<ColumnDetail>());
				}
				return list;
			}
		}

		// Token: 0x17002220 RID: 8736
		// (get) Token: 0x0600CA46 RID: 51782 RVA: 0x002B3B60 File Offset: 0x002B1D60
		protected ILogger Logger { get; }

		// Token: 0x17002221 RID: 8737
		// (get) Token: 0x0600CA47 RID: 51783 RVA: 0x002B3B68 File Offset: 0x002B1D68
		protected ColumnDetail OutputColumnDetail
		{
			get
			{
				ColumnDetail columnDetail;
				if ((columnDetail = this._outputColumnDetail) == null)
				{
					columnDetail = (this._outputColumnDetail = this.Examples.OutputColumnDetail());
				}
				return columnDetail;
			}
		}

		// Token: 0x0600CA48 RID: 51784 RVA: 0x002B3B94 File Offset: 0x002B1D94
		protected void PublishAnomalies()
		{
			if (this.Logger == null || !this._anomalies.Any<string>())
			{
				return;
			}
			Dictionary<string, string> dictionary = this._anomalies.Select((string i, int idx) => new
			{
				Index = idx + 1,
				Value = i
			}).ToDictionary(i => i.Index.ToString(), i => i.Value);
			this.Logger.TrackEvent("TranslateAnomaly", null, null, dictionary);
		}

		// Token: 0x0600CA49 RID: 51785 RVA: 0x002B3C38 File Offset: 0x002B1E38
		protected ColumnDetail ResolveInputColumnDetail(string columnName)
		{
			return this.InputColumnDetails.FirstOrDefault((ColumnDetail d) => d.Name == columnName);
		}

		// Token: 0x0600CA4A RID: 51786 RVA: 0x002B3C69 File Offset: 0x002B1E69
		protected Type ResolveInputType(string columnName)
		{
			ColumnDetail columnDetail = this.ResolveInputColumnDetail(columnName);
			return ((columnDetail != null) ? columnDetail.Type : null) ?? typeof(object);
		}

		// Token: 0x0600CA4B RID: 51787 RVA: 0x002B3C8C File Offset: 0x002B1E8C
		protected void TrackAnomaly(string message)
		{
			this._anomalies.Add(message);
		}

		// Token: 0x0600CA4C RID: 51788 RVA: 0x002B3C9C File Offset: 0x002B1E9C
		protected virtual FormulaExpression Translate(CancellationToken cancellationToken = default(CancellationToken))
		{
			FormulaExpression formulaExpression;
			try
			{
				formulaExpression = this.Translate(this.Program.ProgramNode, cancellationToken);
			}
			finally
			{
				this.PublishAnomalies();
			}
			return formulaExpression;
		}

		// Token: 0x0600CA4D RID: 51789 RVA: 0x00002188 File Offset: 0x00000388
		protected virtual FormulaExpression Translate(ProgramNode node, CancellationToken cancellationToken = default(CancellationToken))
		{
			return null;
		}

		// Token: 0x04004F67 RID: 20327
		private IReadOnlyList<IRow> _allInputs;

		// Token: 0x04004F68 RID: 20328
		private readonly IList<string> _anomalies = new List<string>();

		// Token: 0x04004F69 RID: 20329
		private List<ColumnDetail> _inputColumnDetails;

		// Token: 0x04004F6A RID: 20330
		private ColumnDetail _outputColumnDetail;
	}
}
