using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Properties
{
	// Token: 0x02000067 RID: 103
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x0600021E RID: 542 RVA: 0x00002130 File Offset: 0x00000330
		internal Resources()
		{
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000095CB File Offset: 0x000077CB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Microsoft.ProgramSynthesis.OpenSource.MultiValueDictionary.Resources", typeof(Resources).GetTypeInfo().Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000095FC File Offset: 0x000077FC
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00009603 File Offset: 0x00007803
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000960B File Offset: 0x0000780B
		internal static string Arg_ArrayLengthsDiffer
		{
			get
			{
				return Resources.ResourceManager.GetString("Arg_ArrayLengthsDiffer", Resources.resourceCulture);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00009621 File Offset: 0x00007821
		internal static string Arg_ArrayPlusOffTooSmall
		{
			get
			{
				return Resources.ResourceManager.GetString("Arg_ArrayPlusOffTooSmall", Resources.resourceCulture);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00009637 File Offset: 0x00007837
		internal static string Arg_BitArrayTypeUnsupported
		{
			get
			{
				return Resources.ResourceManager.GetString("Arg_BitArrayTypeUnsupported", Resources.resourceCulture);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000964D File Offset: 0x0000784D
		internal static string Arg_HSCapacityOverflow
		{
			get
			{
				return Resources.ResourceManager.GetString("Arg_HSCapacityOverflow", Resources.resourceCulture);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00009663 File Offset: 0x00007863
		internal static string Arg_HTCapacityOverflow
		{
			get
			{
				return Resources.ResourceManager.GetString("Arg_HTCapacityOverflow", Resources.resourceCulture);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00009679 File Offset: 0x00007879
		internal static string Arg_InsufficientSpace
		{
			get
			{
				return Resources.ResourceManager.GetString("Arg_InsufficientSpace", Resources.resourceCulture);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000968F File Offset: 0x0000788F
		internal static string Arg_MultiRank
		{
			get
			{
				return Resources.ResourceManager.GetString("Arg_MultiRank", Resources.resourceCulture);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000229 RID: 553 RVA: 0x000096A5 File Offset: 0x000078A5
		internal static string Arg_NonZeroLowerBound
		{
			get
			{
				return Resources.ResourceManager.GetString("Arg_NonZeroLowerBound", Resources.resourceCulture);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000096BB File Offset: 0x000078BB
		internal static string Arg_RankMultiDimNotSupported
		{
			get
			{
				return Resources.ResourceManager.GetString("Arg_RankMultiDimNotSupported", Resources.resourceCulture);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000096D1 File Offset: 0x000078D1
		internal static string Arg_WrongType
		{
			get
			{
				return Resources.ResourceManager.GetString("Arg_WrongType", Resources.resourceCulture);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000096E7 File Offset: 0x000078E7
		internal static string Argument_AddingDuplicate
		{
			get
			{
				return Resources.ResourceManager.GetString("Argument_AddingDuplicate", Resources.resourceCulture);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000096FD File Offset: 0x000078FD
		internal static string Argument_ArrayTooLarge
		{
			get
			{
				return Resources.ResourceManager.GetString("Argument_ArrayTooLarge", Resources.resourceCulture);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00009713 File Offset: 0x00007913
		internal static string Argument_ImplementIComparable
		{
			get
			{
				return Resources.ResourceManager.GetString("Argument_ImplementIComparable", Resources.resourceCulture);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00009729 File Offset: 0x00007929
		internal static string Argument_InvalidArgumentForComparison
		{
			get
			{
				return Resources.ResourceManager.GetString("Argument_InvalidArgumentForComparison", Resources.resourceCulture);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000973F File Offset: 0x0000793F
		internal static string Argument_InvalidArrayType
		{
			get
			{
				return Resources.ResourceManager.GetString("Argument_InvalidArrayType", Resources.resourceCulture);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00009755 File Offset: 0x00007955
		internal static string Argument_InvalidOffLen
		{
			get
			{
				return Resources.ResourceManager.GetString("Argument_InvalidOffLen", Resources.resourceCulture);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000976B File Offset: 0x0000796B
		internal static string ArgumentOutOfRange_BiggerThanCollection
		{
			get
			{
				return Resources.ResourceManager.GetString("ArgumentOutOfRange_BiggerThanCollection", Resources.resourceCulture);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00009781 File Offset: 0x00007981
		internal static string ArgumentOutOfRange_Count
		{
			get
			{
				return Resources.ResourceManager.GetString("ArgumentOutOfRange_Count", Resources.resourceCulture);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00009797 File Offset: 0x00007997
		internal static string ArgumentOutOfRange_Index
		{
			get
			{
				return Resources.ResourceManager.GetString("ArgumentOutOfRange_Index", Resources.resourceCulture);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000235 RID: 565 RVA: 0x000097AD File Offset: 0x000079AD
		internal static string ArgumentOutOfRange_ListInsert
		{
			get
			{
				return Resources.ResourceManager.GetString("ArgumentOutOfRange_ListInsert", Resources.resourceCulture);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000236 RID: 566 RVA: 0x000097C3 File Offset: 0x000079C3
		internal static string ArgumentOutOfRange_NeedNonNegNum
		{
			get
			{
				return Resources.ResourceManager.GetString("ArgumentOutOfRange_NeedNonNegNum", Resources.resourceCulture);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000237 RID: 567 RVA: 0x000097D9 File Offset: 0x000079D9
		internal static string ArgumentOutOfRange_NeedNonNegNumRequired
		{
			get
			{
				return Resources.ResourceManager.GetString("ArgumentOutOfRange_NeedNonNegNumRequired", Resources.resourceCulture);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000238 RID: 568 RVA: 0x000097EF File Offset: 0x000079EF
		internal static string ArgumentOutOfRange_SmallCapacity
		{
			get
			{
				return Resources.ResourceManager.GetString("ArgumentOutOfRange_SmallCapacity", Resources.resourceCulture);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00009805 File Offset: 0x00007A05
		internal static string CopyTo_ArgumentsTooSmall
		{
			get
			{
				return Resources.ResourceManager.GetString("CopyTo_ArgumentsTooSmall", Resources.resourceCulture);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000981B File Offset: 0x00007A1B
		internal static string Create_TValueCollectionReadOnly
		{
			get
			{
				return Resources.ResourceManager.GetString("Create_TValueCollectionReadOnly", Resources.resourceCulture);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00009831 File Offset: 0x00007A31
		internal static string ExternalLinkedListNode
		{
			get
			{
				return Resources.ResourceManager.GetString("ExternalLinkedListNode", Resources.resourceCulture);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00009847 File Offset: 0x00007A47
		internal static string IndexOutOfRange
		{
			get
			{
				return Resources.ResourceManager.GetString("IndexOutOfRange", Resources.resourceCulture);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000985D File Offset: 0x00007A5D
		internal static string Invalid_Array_Type
		{
			get
			{
				return Resources.ResourceManager.GetString("Invalid_Array_Type", Resources.resourceCulture);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009873 File Offset: 0x00007A73
		internal static string InvalidOperation_EmptyQueue
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidOperation_EmptyQueue", Resources.resourceCulture);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00009889 File Offset: 0x00007A89
		internal static string InvalidOperation_EmptyStack
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidOperation_EmptyStack", Resources.resourceCulture);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000989F File Offset: 0x00007A9F
		internal static string InvalidOperation_EnumEnded
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidOperation_EnumEnded", Resources.resourceCulture);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000241 RID: 577 RVA: 0x000098B5 File Offset: 0x00007AB5
		internal static string InvalidOperation_EnumFailedVersion
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidOperation_EnumFailedVersion", Resources.resourceCulture);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000242 RID: 578 RVA: 0x000098CB File Offset: 0x00007ACB
		internal static string InvalidOperation_EnumNotStarted
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidOperation_EnumNotStarted", Resources.resourceCulture);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000243 RID: 579 RVA: 0x000098E1 File Offset: 0x00007AE1
		internal static string InvalidOperation_EnumOpCantHappen
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidOperation_EnumOpCantHappen", Resources.resourceCulture);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000098F7 File Offset: 0x00007AF7
		internal static string LinkedListEmpty
		{
			get
			{
				return Resources.ResourceManager.GetString("LinkedListEmpty", Resources.resourceCulture);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000990D File Offset: 0x00007B0D
		internal static string LinkedListNodeIsAttached
		{
			get
			{
				return Resources.ResourceManager.GetString("LinkedListNodeIsAttached", Resources.resourceCulture);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00009923 File Offset: 0x00007B23
		internal static string NotSupported_KeyCollectionSet
		{
			get
			{
				return Resources.ResourceManager.GetString("NotSupported_KeyCollectionSet", Resources.resourceCulture);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000247 RID: 583 RVA: 0x00009939 File Offset: 0x00007B39
		internal static string NotSupported_SortedListNestedWrite
		{
			get
			{
				return Resources.ResourceManager.GetString("NotSupported_SortedListNestedWrite", Resources.resourceCulture);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000994F File Offset: 0x00007B4F
		internal static string NotSupported_ValueCollectionSet
		{
			get
			{
				return Resources.ResourceManager.GetString("NotSupported_ValueCollectionSet", Resources.resourceCulture);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00009965 File Offset: 0x00007B65
		internal static string ReadOnly_Modification
		{
			get
			{
				return Resources.ResourceManager.GetString("ReadOnly_Modification", Resources.resourceCulture);
			}
		}

		// Token: 0x04000127 RID: 295
		private static ResourceManager resourceMan;

		// Token: 0x04000128 RID: 296
		private static CultureInfo resourceCulture;
	}
}
