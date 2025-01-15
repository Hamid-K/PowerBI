using System;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000133 RID: 307
	internal sealed class BinaryOrderedUdtNormalizer : Normalizer
	{
		// Token: 0x06001848 RID: 6216 RVA: 0x0006575D File Offset: 0x0006395D
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private FieldInfo[] GetFields(Type t)
		{
			return t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00065768 File Offset: 0x00063968
		internal BinaryOrderedUdtNormalizer(Type t, bool isTopLevelUdt)
		{
			this._skipNormalize = false;
			if (this._skipNormalize)
			{
				this._isTopLevelUdt = true;
			}
			this._isTopLevelUdt = true;
			FieldInfo[] fields = this.GetFields(t);
			this._fieldsToNormalize = new FieldInfoEx[fields.Length];
			int num = 0;
			foreach (FieldInfo fieldInfo in fields)
			{
				int num2 = Marshal.OffsetOf(fieldInfo.DeclaringType, fieldInfo.Name).ToInt32();
				this._fieldsToNormalize[num++] = new FieldInfoEx(fieldInfo, num2, Normalizer.GetNormalizer(fieldInfo.FieldType));
			}
			Array.Sort<FieldInfoEx>(this._fieldsToNormalize);
			if (!this._isTopLevelUdt && typeof(INullable).IsAssignableFrom(t))
			{
				PropertyInfo property = t.GetProperty("Null", BindingFlags.Static | BindingFlags.Public);
				if (property == null || property.PropertyType != t)
				{
					FieldInfo field = t.GetField("Null", BindingFlags.Static | BindingFlags.Public);
					if (field == null || field.FieldType != t)
					{
						throw new Exception("could not find Null field/property in nullable type " + ((t != null) ? t.ToString() : null));
					}
					this._nullInstance = field.GetValue(null);
				}
				else
				{
					this._nullInstance = property.GetValue(null, null);
				}
				this._padBuffer = new byte[this.Size - 1];
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x0600184A RID: 6218 RVA: 0x000658CE File Offset: 0x00063ACE
		internal bool IsNullable
		{
			get
			{
				return this._nullInstance != null;
			}
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x000658D9 File Offset: 0x00063AD9
		internal void NormalizeTopObject(object udt, Stream s)
		{
			this.Normalize(null, udt, s);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x000658E4 File Offset: 0x00063AE4
		internal object DeNormalizeTopObject(Type t, Stream s)
		{
			return this.DeNormalizeInternal(t, s);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x000658F0 File Offset: 0x00063AF0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private object DeNormalizeInternal(Type t, Stream s)
		{
			object obj = null;
			if (!this._isTopLevelUdt && typeof(INullable).IsAssignableFrom(t) && (byte)s.ReadByte() == 0)
			{
				obj = this._nullInstance;
				s.Read(this._padBuffer, 0, this._padBuffer.Length);
				return obj;
			}
			if (obj == null)
			{
				obj = Activator.CreateInstance(t);
			}
			foreach (FieldInfoEx fieldInfoEx in this._fieldsToNormalize)
			{
				fieldInfoEx.Normalizer.DeNormalize(fieldInfoEx.FieldInfo, obj, s);
			}
			return obj;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00065980 File Offset: 0x00063B80
		internal override void Normalize(FieldInfo fi, object obj, Stream s)
		{
			object obj2;
			if (fi == null)
			{
				obj2 = obj;
			}
			else
			{
				obj2 = base.GetValue(fi, obj);
			}
			INullable nullable = obj2 as INullable;
			if (nullable != null && !this._isTopLevelUdt)
			{
				if (nullable.IsNull)
				{
					s.WriteByte(0);
					s.Write(this._padBuffer, 0, this._padBuffer.Length);
					return;
				}
				s.WriteByte(1);
			}
			foreach (FieldInfoEx fieldInfoEx in this._fieldsToNormalize)
			{
				fieldInfoEx.Normalizer.Normalize(fieldInfoEx.FieldInfo, obj2, s);
			}
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00065A10 File Offset: 0x00063C10
		internal override void DeNormalize(FieldInfo fi, object recvr, Stream s)
		{
			base.SetValue(fi, recvr, this.DeNormalizeInternal(fi.FieldType, s));
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x00065A28 File Offset: 0x00063C28
		internal override int Size
		{
			get
			{
				if (this._size != 0)
				{
					return this._size;
				}
				if (this.IsNullable && !this._isTopLevelUdt)
				{
					this._size = 1;
				}
				foreach (FieldInfoEx fieldInfoEx in this._fieldsToNormalize)
				{
					this._size += fieldInfoEx.Normalizer.Size;
				}
				return this._size;
			}
		}

		// Token: 0x040009B2 RID: 2482
		private readonly FieldInfoEx[] _fieldsToNormalize;

		// Token: 0x040009B3 RID: 2483
		private int _size;

		// Token: 0x040009B4 RID: 2484
		private readonly byte[] _padBuffer;

		// Token: 0x040009B5 RID: 2485
		private readonly object _nullInstance;

		// Token: 0x040009B6 RID: 2486
		private readonly bool _isTopLevelUdt;
	}
}
