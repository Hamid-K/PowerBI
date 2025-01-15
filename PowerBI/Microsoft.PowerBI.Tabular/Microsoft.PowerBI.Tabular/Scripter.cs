using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.AnalysisServices.Tabular.DDL;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000115 RID: 277
	public static class Scripter
	{
		// Token: 0x060011F4 RID: 4596 RVA: 0x0007E714 File Offset: 0x0007C914
		public static IEnumerable<XElement> ScriptCreateModelAsXElements(Model model, string databaseId, CompatibilityMode mode = CompatibilityMode.Unknown)
		{
			if (mode == CompatibilityMode.Unknown && !model.TryGetCurrentMode(out mode))
			{
				mode = CompatibilityMode.PowerBI;
			}
			int num = model.GetCompatibilityRequirementLevel(mode);
			if (num == -1)
			{
				num = 1200;
			}
			return Scripter.ScriptCreateModelAsXElements(model, databaseId, num, mode);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0007E74C File Offset: 0x0007C94C
		public static IEnumerable<XElement> ScriptCreateModelAsXElements(Model model, string databaseId, int dbCompatibilityLevel, CompatibilityMode mode = CompatibilityMode.Unknown)
		{
			if (string.IsNullOrEmpty(databaseId))
			{
				throw new ArgumentNullException("databaseId");
			}
			if (dbCompatibilityLevel < 1200)
			{
				throw new ArgumentOutOfRangeException("dbCompatibilityLevel");
			}
			if (mode == CompatibilityMode.Unknown && !model.TryGetCurrentMode(out mode))
			{
				mode = CompatibilityMode.PowerBI;
			}
			int num;
			string text;
			model.GetCompatibilityRequirement(mode, out num, out text);
			if (dbCompatibilityLevel < num || num == -2)
			{
				throw new CompatibilityViolationException(mode, dbCompatibilityLevel, num, text);
			}
			XElement xelement = DdlUtil.FormatAlterSingleObject(databaseId, mode, dbCompatibilityLevel, model);
			XElement createChildrenObjectsStatementElement = null;
			if (model.GetAllDescendants().Any<MetadataObject>())
			{
				createChildrenObjectsStatementElement = DdlUtil.FormatCreate(WriteOptions.WriteObjectPath, databaseId, mode, dbCompatibilityLevel, model.GetAllDescendants().ToList<MetadataObject>());
			}
			yield return xelement;
			if (createChildrenObjectsStatementElement != null)
			{
				yield return createChildrenObjectsStatementElement;
			}
			yield break;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0007E774 File Offset: 0x0007C974
		public static XElement ScriptUpgradeModelAsXElement(Model model, string databaseId)
		{
			if (string.IsNullOrEmpty(databaseId))
			{
				throw new ArgumentNullException("databaseId");
			}
			List<MetadataObject> list = new List<MetadataObject>();
			list.Add(model);
			list.AddRange(model.GetAllDescendants());
			XElement xelement = null;
			if (list.Any<MetadataObject>())
			{
				xelement = DdlUtil.FormatUpgrade(WriteOptions.WriteObjectPath, databaseId, CompatibilityMode.AnalysisServices, 1200, list);
			}
			return xelement;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0007E7C7 File Offset: 0x0007C9C7
		public static XElement ScriptBatchAsXElement(bool isTransactional)
		{
			return DdlUtil.GetBatchElement(isTransactional);
		}
	}
}
