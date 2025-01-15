using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D0E RID: 7438
	internal class RelationshipIdentityAscriber
	{
		// Token: 0x0600B985 RID: 47493 RVA: 0x002596D6 File Offset: 0x002578D6
		public RelationshipIdentityAscriber(IEngine engine)
		{
			this.engine = engine;
		}

		// Token: 0x0600B986 RID: 47494 RVA: 0x002596E8 File Offset: 0x002578E8
		public IPartitionedDocument AscribeRelationshipIdentity(IPartitionedDocument document)
		{
			List<PackageEdit> list = new List<PackageEdit>();
			foreach (IPartitionKey partitionKey in document.PartitionKeys)
			{
				if (!document.IsPartitionInError(partitionKey))
				{
					string text = "Table.ReplaceRelationshipIdentity(";
					string text2 = string.Format(CultureInfo.InvariantCulture, ", {0})", this.engine.EscapeString(partitionKey.ToSerializedString()));
					int num;
					int num2;
					string partitionSectionOffsetAndLength = document.GetPartitionSectionOffsetAndLength(partitionKey, out num, out num2);
					list.Add(new PackageEdit(partitionSectionOffsetAndLength, num, 0, SegmentedString.New(text)));
					list.Add(new PackageEdit(partitionSectionOffsetAndLength, num + num2, 0, SegmentedString.New(text2)));
				}
			}
			return new EditsPartitionedDocument(this.engine, document, list);
		}

		// Token: 0x04005E73 RID: 24179
		private const string headerTemplate = "Table.ReplaceRelationshipIdentity(";

		// Token: 0x04005E74 RID: 24180
		private const string footerTemplate = ", {0})";

		// Token: 0x04005E75 RID: 24181
		private readonly IEngine engine;
	}
}
