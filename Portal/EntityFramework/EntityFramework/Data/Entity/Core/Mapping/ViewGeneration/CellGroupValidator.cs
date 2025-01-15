using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Validation;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x0200056B RID: 1387
	internal class CellGroupValidator
	{
		// Token: 0x06004387 RID: 17287 RVA: 0x000EA68F File Offset: 0x000E888F
		internal CellGroupValidator(IEnumerable<Cell> cells, ConfigViewGenerator config)
		{
			this.m_cells = cells;
			this.m_config = config;
			this.m_errorLog = new ErrorLog();
		}

		// Token: 0x06004388 RID: 17288 RVA: 0x000EA6B0 File Offset: 0x000E88B0
		internal ErrorLog Validate()
		{
			if (this.m_config.IsValidationEnabled)
			{
				if (!this.PerformSingleCellChecks())
				{
					return this.m_errorLog;
				}
			}
			else if (!this.CheckCellsWithDistinctFlag())
			{
				return this.m_errorLog;
			}
			SchemaConstraints<BasicKeyConstraint> schemaConstraints = new SchemaConstraints<BasicKeyConstraint>();
			SchemaConstraints<BasicKeyConstraint> schemaConstraints2 = new SchemaConstraints<BasicKeyConstraint>();
			this.ConstructCellRelationsWithConstraints(schemaConstraints, schemaConstraints2);
			if (this.m_config.IsVerboseTracing)
			{
				Trace.WriteLine(string.Empty);
				Trace.WriteLine("C-Level Basic Constraints");
				Trace.WriteLine(schemaConstraints);
				Trace.WriteLine("S-Level Basic Constraints");
				Trace.WriteLine(schemaConstraints2);
			}
			this.m_cViewConstraints = CellGroupValidator.PropagateConstraints(schemaConstraints);
			this.m_sViewConstraints = CellGroupValidator.PropagateConstraints(schemaConstraints2);
			if (this.m_config.IsVerboseTracing)
			{
				Trace.WriteLine(string.Empty);
				Trace.WriteLine("C-Level View Constraints");
				Trace.WriteLine(this.m_cViewConstraints);
				Trace.WriteLine("S-Level View Constraints");
				Trace.WriteLine(this.m_sViewConstraints);
			}
			if (this.m_config.IsValidationEnabled)
			{
				this.CheckImplication(this.m_cViewConstraints, this.m_sViewConstraints);
			}
			return this.m_errorLog;
		}

		// Token: 0x06004389 RID: 17289 RVA: 0x000EA7B4 File Offset: 0x000E89B4
		private void ConstructCellRelationsWithConstraints(SchemaConstraints<BasicKeyConstraint> cConstraints, SchemaConstraints<BasicKeyConstraint> sConstraints)
		{
			int num = 0;
			foreach (Cell cell in this.m_cells)
			{
				cell.CreateViewCellRelation(num);
				BasicCellRelation basicCellRelation = cell.CQuery.BasicCellRelation;
				BasicCellRelation basicCellRelation2 = cell.SQuery.BasicCellRelation;
				CellGroupValidator.PopulateBaseConstraints(basicCellRelation, cConstraints);
				CellGroupValidator.PopulateBaseConstraints(basicCellRelation2, sConstraints);
				num++;
			}
			foreach (Cell cell2 in this.m_cells)
			{
				foreach (Cell cell3 in this.m_cells)
				{
				}
			}
		}

		// Token: 0x0600438A RID: 17290 RVA: 0x000EA8A0 File Offset: 0x000E8AA0
		private static void PopulateBaseConstraints(BasicCellRelation baseRelation, SchemaConstraints<BasicKeyConstraint> constraints)
		{
			baseRelation.PopulateKeyConstraints(constraints);
		}

		// Token: 0x0600438B RID: 17291 RVA: 0x000EA8AC File Offset: 0x000E8AAC
		private static SchemaConstraints<ViewKeyConstraint> PropagateConstraints(SchemaConstraints<BasicKeyConstraint> baseConstraints)
		{
			SchemaConstraints<ViewKeyConstraint> schemaConstraints = new SchemaConstraints<ViewKeyConstraint>();
			foreach (BasicKeyConstraint basicKeyConstraint in baseConstraints.KeyConstraints)
			{
				ViewKeyConstraint viewKeyConstraint = basicKeyConstraint.Propagate();
				if (viewKeyConstraint != null)
				{
					schemaConstraints.Add(viewKeyConstraint);
				}
			}
			return schemaConstraints;
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x000EA908 File Offset: 0x000E8B08
		private void CheckImplication(SchemaConstraints<ViewKeyConstraint> cViewConstraints, SchemaConstraints<ViewKeyConstraint> sViewConstraints)
		{
			this.CheckImplicationKeyConstraints(cViewConstraints, sViewConstraints);
			KeyToListMap<CellGroupValidator.ExtentPair, ViewKeyConstraint> keyToListMap = new KeyToListMap<CellGroupValidator.ExtentPair, ViewKeyConstraint>(EqualityComparer<CellGroupValidator.ExtentPair>.Default);
			foreach (ViewKeyConstraint viewKeyConstraint in cViewConstraints.KeyConstraints)
			{
				CellGroupValidator.ExtentPair extentPair = new CellGroupValidator.ExtentPair(viewKeyConstraint.Cell.CQuery.Extent, viewKeyConstraint.Cell.SQuery.Extent);
				keyToListMap.Add(extentPair, viewKeyConstraint);
			}
			foreach (CellGroupValidator.ExtentPair extentPair2 in keyToListMap.Keys)
			{
				ReadOnlyCollection<ViewKeyConstraint> readOnlyCollection = keyToListMap.ListForKey(extentPair2);
				bool flag = false;
				foreach (ViewKeyConstraint viewKeyConstraint2 in readOnlyCollection)
				{
					using (IEnumerator<ViewKeyConstraint> enumerator3 = sViewConstraints.KeyConstraints.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							if (enumerator3.Current.Implies(viewKeyConstraint2))
							{
								flag = true;
								break;
							}
						}
					}
				}
				if (!flag)
				{
					this.m_errorLog.AddEntry(ViewKeyConstraint.GetErrorRecord(readOnlyCollection));
				}
			}
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x000EAA6C File Offset: 0x000E8C6C
		private void CheckImplicationKeyConstraints(SchemaConstraints<ViewKeyConstraint> leftViewConstraints, SchemaConstraints<ViewKeyConstraint> rightViewConstraints)
		{
			foreach (ViewKeyConstraint viewKeyConstraint in rightViewConstraints.KeyConstraints)
			{
				bool flag = false;
				using (IEnumerator<ViewKeyConstraint> enumerator2 = leftViewConstraints.KeyConstraints.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.Implies(viewKeyConstraint))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					this.m_errorLog.AddEntry(ViewKeyConstraint.GetErrorRecord(viewKeyConstraint));
				}
			}
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x000EAB08 File Offset: 0x000E8D08
		private bool CheckCellsWithDistinctFlag()
		{
			int count = this.m_errorLog.Count;
			using (IEnumerator<Cell> enumerator = this.m_cells.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Cell cell = enumerator.Current;
					if (cell.SQuery.SelectDistinctFlag == CellQuery.SelectDistinct.Yes)
					{
						EntitySetBase cExtent = cell.CQuery.Extent;
						EntitySetBase sExtent = cell.SQuery.Extent;
						IEnumerable<Cell> enumerable = from otherCell in this.m_cells
							where otherCell != cell
							where otherCell.CQuery.Extent == cExtent && otherCell.SQuery.Extent == sExtent
							select otherCell;
						if (enumerable.Any<Cell>())
						{
							IEnumerable<Cell> enumerable2 = Enumerable.Repeat<Cell>(cell, 1).Union(enumerable);
							ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.MultipleFragmentsBetweenCandSExtentWithDistinct, Strings.Viewgen_MultipleFragmentsBetweenCandSExtentWithDistinct(cExtent.Name, sExtent.Name), enumerable2, string.Empty);
							this.m_errorLog.AddEntry(record);
						}
					}
				}
			}
			return this.m_errorLog.Count == count;
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x000EAC44 File Offset: 0x000E8E44
		private bool PerformSingleCellChecks()
		{
			int count = this.m_errorLog.Count;
			foreach (Cell cell in this.m_cells)
			{
				ErrorLog.Record record = cell.SQuery.CheckForDuplicateFields(cell.CQuery, cell);
				if (record != null)
				{
					this.m_errorLog.AddEntry(record);
				}
				record = cell.CQuery.VerifyKeysPresent(cell, new Func<object, object, string>(Strings.ViewGen_EntitySetKey_Missing), new Func<object, object, object, string>(Strings.ViewGen_AssociationSetKey_Missing), ViewGenErrorCode.KeyNotMappedForCSideExtent);
				if (record != null)
				{
					this.m_errorLog.AddEntry(record);
				}
				record = cell.SQuery.VerifyKeysPresent(cell, new Func<object, object, string>(Strings.ViewGen_TableKey_Missing), null, ViewGenErrorCode.KeyNotMappedForTable);
				if (record != null)
				{
					this.m_errorLog.AddEntry(record);
				}
				record = cell.CQuery.CheckForProjectedNotNullSlots(cell, this.m_cells.Where((Cell c) => c.SQuery.Extent is AssociationSet));
				if (record != null)
				{
					this.m_errorLog.AddEntry(record);
				}
				record = cell.SQuery.CheckForProjectedNotNullSlots(cell, this.m_cells.Where((Cell c) => c.CQuery.Extent is AssociationSet));
				if (record != null)
				{
					this.m_errorLog.AddEntry(record);
				}
			}
			return this.m_errorLog.Count == count;
		}

		// Token: 0x06004390 RID: 17296 RVA: 0x000EADCC File Offset: 0x000E8FCC
		[Conditional("DEBUG")]
		private static void CheckConstraintSanity(SchemaConstraints<BasicKeyConstraint> cConstraints, SchemaConstraints<BasicKeyConstraint> sConstraints, SchemaConstraints<ViewKeyConstraint> cViewConstraints, SchemaConstraints<ViewKeyConstraint> sViewConstraints)
		{
		}

		// Token: 0x04001835 RID: 6197
		private readonly IEnumerable<Cell> m_cells;

		// Token: 0x04001836 RID: 6198
		private readonly ConfigViewGenerator m_config;

		// Token: 0x04001837 RID: 6199
		private readonly ErrorLog m_errorLog;

		// Token: 0x04001838 RID: 6200
		private SchemaConstraints<ViewKeyConstraint> m_cViewConstraints;

		// Token: 0x04001839 RID: 6201
		private SchemaConstraints<ViewKeyConstraint> m_sViewConstraints;

		// Token: 0x02000B79 RID: 2937
		private class ExtentPair
		{
			// Token: 0x0600664B RID: 26187 RVA: 0x0015F52B File Offset: 0x0015D72B
			internal ExtentPair(EntitySetBase acExtent, EntitySetBase asExtent)
			{
				this.cExtent = acExtent;
				this.sExtent = asExtent;
			}

			// Token: 0x0600664C RID: 26188 RVA: 0x0015F544 File Offset: 0x0015D744
			public override bool Equals(object obj)
			{
				if (this == obj)
				{
					return true;
				}
				CellGroupValidator.ExtentPair extentPair = obj as CellGroupValidator.ExtentPair;
				return extentPair != null && extentPair.cExtent.Equals(this.cExtent) && extentPair.sExtent.Equals(this.sExtent);
			}

			// Token: 0x0600664D RID: 26189 RVA: 0x0015F589 File Offset: 0x0015D789
			public override int GetHashCode()
			{
				return this.cExtent.GetHashCode() ^ this.sExtent.GetHashCode();
			}

			// Token: 0x04002DE0 RID: 11744
			internal readonly EntitySetBase cExtent;

			// Token: 0x04002DE1 RID: 11745
			internal readonly EntitySetBase sExtent;
		}
	}
}
