using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200017D RID: 381
	[DomName("CharacterData")]
	public interface ICharacterData : INode, IEventTarget, IMarkupFormattable, IChildNode, INonDocumentTypeChildNode
	{
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000DB0 RID: 3504
		// (set) Token: 0x06000DB1 RID: 3505
		[DomName("data")]
		string Data { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000DB2 RID: 3506
		[DomName("length")]
		int Length { get; }

		// Token: 0x06000DB3 RID: 3507
		[DomName("substringData")]
		string Substring(int offset, int count);

		// Token: 0x06000DB4 RID: 3508
		[DomName("appendData")]
		void Append(string value);

		// Token: 0x06000DB5 RID: 3509
		[DomName("insertData")]
		void Insert(int offset, string value);

		// Token: 0x06000DB6 RID: 3510
		[DomName("deleteData")]
		void Delete(int offset, int count);

		// Token: 0x06000DB7 RID: 3511
		[DomName("replaceData")]
		void Replace(int offset, int count, string value);
	}
}
