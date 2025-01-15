using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001986 RID: 6534
	public sealed class StackFrameExtendedInfo : IGetStackFrameExtendedInfo
	{
		// Token: 0x0600A5B6 RID: 42422 RVA: 0x00224918 File Offset: 0x00222B18
		public StackFrameExtendedInfo(IPartitionedDocument document)
		{
			this.document = document;
		}

		// Token: 0x0600A5B7 RID: 42423 RVA: 0x00224928 File Offset: 0x00222B28
		public IRecordValue GetStackFrameExtendedInfo(IEngine engine, IValue frameLocation, IValue sectionName)
		{
			string text;
			string text2;
			this.GetMemberAndLetNames(frameLocation.AsRecord, sectionName.AsString, out text, out text2);
			List<string> list = new List<string>();
			List<IValue> list2 = new List<IValue>();
			list.Add("Section");
			list2.Add(sectionName);
			if (text != null)
			{
				list.Add("Member");
				list2.Add(engine.Text(text));
			}
			if (text2 != null)
			{
				list.Add("Let");
				list2.Add(engine.Text(text2));
			}
			return engine.Record(engine.Keys(list.ToArray()), list2.ToArray());
		}

		// Token: 0x0600A5B8 RID: 42424 RVA: 0x002249B8 File Offset: 0x00222BB8
		private void GetMemberAndLetNames(IRecordValue location, string sectionName, out string memberName, out string letName)
		{
			TextPosition textPosition = this.GetTextPosition(location["Start"]);
			TextPosition textPosition2 = this.GetTextPosition(location["End"]);
			int num;
			int num2;
			if (this.document.TryGetOffsetAndLength(sectionName, new TextRange(textPosition, textPosition2), out num, out num2))
			{
				int num3;
				IMemberLetPartitionKey memberLetPartitionKey = this.document.GetPartitionKeyAndOffset(sectionName, num, num2, out num3) as IMemberLetPartitionKey;
				if (memberLetPartitionKey != null && memberLetPartitionKey.Lets.Count > 0)
				{
					memberName = memberLetPartitionKey.Member;
					letName = memberLetPartitionKey.Lets[0];
					return;
				}
			}
			memberName = string.Empty;
			letName = string.Empty;
		}

		// Token: 0x0600A5B9 RID: 42425 RVA: 0x00224A58 File Offset: 0x00222C58
		private TextPosition GetTextPosition(IValue position)
		{
			if (position.IsNull)
			{
				return new TextPosition(0, 0);
			}
			return new TextPosition(position.AsRecord["Row"].AsNumber.AsInteger32, position.AsRecord["Column"].AsNumber.AsInteger32);
		}

		// Token: 0x04005645 RID: 22085
		private const string LocationSection = "Section";

		// Token: 0x04005646 RID: 22086
		public const string LocationMember = "Member";

		// Token: 0x04005647 RID: 22087
		public const string LocationLet = "Let";

		// Token: 0x04005648 RID: 22088
		private const string LocationStart = "Start";

		// Token: 0x04005649 RID: 22089
		private const string LocationEnd = "End";

		// Token: 0x0400564A RID: 22090
		private const string PositionRow = "Row";

		// Token: 0x0400564B RID: 22091
		private const string PositionColumn = "Column";

		// Token: 0x0400564C RID: 22092
		private readonly IPartitionedDocument document;
	}
}
