using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x0200025E RID: 606
	internal abstract class KeySerializer
	{
		// Token: 0x06001B4C RID: 6988 RVA: 0x0005430A File Offset: 0x0005250A
		internal static KeySerializer Create(bool enableKeyAsSegment)
		{
			if (enableKeyAsSegment)
			{
				return KeySerializer.SegmentInstance;
			}
			return KeySerializer.DefaultInstance;
		}

		// Token: 0x06001B4D RID: 6989
		internal abstract void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue);

		// Token: 0x06001B4E RID: 6990 RVA: 0x0005431C File Offset: 0x0005251C
		private static string GetKeyValueAsString<TProperty>(Func<TProperty, object> getPropertyValue, TProperty property, LiteralFormatter literalFormatter)
		{
			object obj = getPropertyValue(property);
			return literalFormatter.Format(obj);
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0005433C File Offset: 0x0005253C
		private static void AppendKeyWithParentheses<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue)
		{
			LiteralFormatter literalFormatter = LiteralFormatter.ForKeys(false);
			builder.Append('(');
			bool flag = true;
			foreach (TProperty tproperty in keyProperties)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					builder.Append(',');
				}
				if (keyProperties.Count != 1)
				{
					builder.Append(getPropertyName(tproperty));
					builder.Append('=');
				}
				string keyValueAsString = KeySerializer.GetKeyValueAsString<TProperty>(getPropertyValue, tproperty, literalFormatter);
				builder.Append(keyValueAsString);
			}
			builder.Append(')');
		}

		// Token: 0x04000B76 RID: 2934
		private static readonly KeySerializer.DefaultKeySerializer DefaultInstance = new KeySerializer.DefaultKeySerializer();

		// Token: 0x04000B77 RID: 2935
		private static readonly KeySerializer.SegmentKeySerializer SegmentInstance = new KeySerializer.SegmentKeySerializer();

		// Token: 0x0200044D RID: 1101
		private sealed class DefaultKeySerializer : KeySerializer
		{
			// Token: 0x060021F1 RID: 8689 RVA: 0x0005E6D2 File Offset: 0x0005C8D2
			internal override void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue)
			{
				KeySerializer.AppendKeyWithParentheses<TProperty>(builder, keyProperties, getPropertyName, getPropertyValue);
			}
		}

		// Token: 0x0200044E RID: 1102
		private sealed class SegmentKeySerializer : KeySerializer
		{
			// Token: 0x060021F3 RID: 8691 RVA: 0x0005E6DE File Offset: 0x0005C8DE
			internal SegmentKeySerializer()
			{
			}

			// Token: 0x060021F4 RID: 8692 RVA: 0x0005E6E6 File Offset: 0x0005C8E6
			internal override void AppendKeyExpression<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, string> getPropertyName, Func<TProperty, object> getPropertyValue)
			{
				if (keyProperties.Count > 1)
				{
					KeySerializer.AppendKeyWithParentheses<TProperty>(builder, keyProperties, getPropertyName, getPropertyValue);
					return;
				}
				KeySerializer.SegmentKeySerializer.AppendKeyWithSegments<TProperty>(builder, keyProperties, getPropertyValue);
			}

			// Token: 0x060021F5 RID: 8693 RVA: 0x0005E705 File Offset: 0x0005C905
			private static void AppendKeyWithSegments<TProperty>(StringBuilder builder, ICollection<TProperty> keyProperties, Func<TProperty, object> getPropertyValue)
			{
				builder.Append('/');
				builder.Append(KeySerializer.GetKeyValueAsString<TProperty>(getPropertyValue, keyProperties.Single<TProperty>(), LiteralFormatter.ForKeys(true)));
			}
		}
	}
}
