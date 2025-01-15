using System;
using AngleSharp.Dom;

namespace AngleSharp.Extensions
{
	// Token: 0x020000E3 RID: 227
	internal static class AttrExtensions
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x00032A08 File Offset: 0x00030C08
		public static bool AreEqual(this INamedNodeMap sourceAttributes, INamedNodeMap targetAttributes)
		{
			if (sourceAttributes.Length == targetAttributes.Length)
			{
				foreach (IAttr attr in sourceAttributes)
				{
					bool flag = false;
					foreach (IAttr attr2 in targetAttributes)
					{
						flag = attr.GetHashCode() == attr2.GetHashCode();
						if (flag)
						{
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}
	}
}
