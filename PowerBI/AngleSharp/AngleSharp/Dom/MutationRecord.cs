using System;
using AngleSharp.Extensions;

namespace AngleSharp.Dom
{
	// Token: 0x0200015B RID: 347
	internal sealed class MutationRecord : IMutationRecord
	{
		// Token: 0x06000BF6 RID: 3062 RVA: 0x00006EB6 File Offset: 0x000050B6
		private MutationRecord()
		{
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000445BD File Offset: 0x000427BD
		public static MutationRecord CharacterData(INode target, string previousValue = null)
		{
			return new MutationRecord
			{
				Type = MutationRecord.CharacterDataType,
				Target = target,
				PreviousValue = previousValue
			};
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000445DD File Offset: 0x000427DD
		public static MutationRecord ChildList(INode target, INodeList addedNodes = null, INodeList removedNodes = null, INode previousSibling = null, INode nextSibling = null)
		{
			return new MutationRecord
			{
				Type = MutationRecord.ChildListType,
				Target = target,
				Added = addedNodes,
				Removed = removedNodes,
				PreviousSibling = previousSibling,
				NextSibling = nextSibling
			};
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00044613 File Offset: 0x00042813
		public static MutationRecord Attributes(INode target, string attributeName = null, string attributeNamespace = null, string previousValue = null)
		{
			return new MutationRecord
			{
				Type = MutationRecord.AttributesType,
				Target = target,
				AttributeName = attributeName,
				AttributeNamespace = attributeNamespace,
				PreviousValue = previousValue
			};
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00044644 File Offset: 0x00042844
		public MutationRecord Copy(bool clearPreviousValue)
		{
			return new MutationRecord
			{
				Type = this.Type,
				Target = this.Target,
				PreviousSibling = this.PreviousSibling,
				NextSibling = this.NextSibling,
				AttributeName = this.AttributeName,
				AttributeNamespace = this.AttributeNamespace,
				PreviousValue = (clearPreviousValue ? null : this.PreviousValue),
				Added = this.Added,
				Removed = this.Removed
			};
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x000446C8 File Offset: 0x000428C8
		public bool IsAttribute
		{
			get
			{
				return this.Type.Is(MutationRecord.AttributesType);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x000446DA File Offset: 0x000428DA
		public bool IsCharacterData
		{
			get
			{
				return this.Type.Is(MutationRecord.CharacterDataType);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x000446EC File Offset: 0x000428EC
		public bool IsChildList
		{
			get
			{
				return this.Type.Is(MutationRecord.ChildListType);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x000446FE File Offset: 0x000428FE
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x00044706 File Offset: 0x00042906
		public string Type { get; private set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0004470F File Offset: 0x0004290F
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x00044717 File Offset: 0x00042917
		public INode Target { get; private set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00044720 File Offset: 0x00042920
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00044728 File Offset: 0x00042928
		public INodeList Added { get; private set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00044731 File Offset: 0x00042931
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00044739 File Offset: 0x00042939
		public INodeList Removed { get; private set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00044742 File Offset: 0x00042942
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x0004474A File Offset: 0x0004294A
		public INode PreviousSibling { get; private set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00044753 File Offset: 0x00042953
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x0004475B File Offset: 0x0004295B
		public INode NextSibling { get; private set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00044764 File Offset: 0x00042964
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x0004476C File Offset: 0x0004296C
		public string AttributeName { get; private set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00044775 File Offset: 0x00042975
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x0004477D File Offset: 0x0004297D
		public string AttributeNamespace { get; private set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00044786 File Offset: 0x00042986
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x0004478E File Offset: 0x0004298E
		public string PreviousValue { get; private set; }

		// Token: 0x04000951 RID: 2385
		private static readonly string CharacterDataType = "characterData";

		// Token: 0x04000952 RID: 2386
		private static readonly string AttributesType = "attributes";

		// Token: 0x04000953 RID: 2387
		private static readonly string ChildListType = "childList";
	}
}
