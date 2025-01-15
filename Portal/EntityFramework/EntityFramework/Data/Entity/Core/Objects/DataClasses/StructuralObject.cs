using System;
using System.ComponentModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000484 RID: 1156
	[DataContract(IsReference = true)]
	[Serializable]
	public abstract class StructuralObject : INotifyPropertyChanging, INotifyPropertyChanged
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06003919 RID: 14617 RVA: 0x000BCFC0 File Offset: 0x000BB1C0
		// (remove) Token: 0x0600391A RID: 14618 RVA: 0x000BCFF8 File Offset: 0x000BB1F8
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600391B RID: 14619 RVA: 0x000BD030 File Offset: 0x000BB230
		// (remove) Token: 0x0600391C RID: 14620 RVA: 0x000BD068 File Offset: 0x000BB268
		[field: NonSerialized]
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x0600391D RID: 14621 RVA: 0x000BD09D File Offset: 0x000BB29D
		protected virtual void OnPropertyChanged(string property)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(property));
			}
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000BD0B9 File Offset: 0x000BB2B9
		protected virtual void OnPropertyChanging(string property)
		{
			if (this.PropertyChanging != null)
			{
				this.PropertyChanging(this, new PropertyChangingEventArgs(property));
			}
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000BD0D5 File Offset: 0x000BB2D5
		protected static DateTime DefaultDateTimeValue()
		{
			return DateTime.Now;
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x000BD0DC File Offset: 0x000BB2DC
		protected virtual void ReportPropertyChanging(string property)
		{
			Check.NotEmpty(property, "property");
			this.OnPropertyChanging(property);
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000BD0F1 File Offset: 0x000BB2F1
		protected virtual void ReportPropertyChanged(string property)
		{
			Check.NotEmpty(property, "property");
			this.OnPropertyChanged(property);
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000BD106 File Offset: 0x000BB306
		protected internal T GetValidValue<T>(T currentValue, string property, bool isNullable, bool isInitialized) where T : ComplexObject, new()
		{
			if (!isNullable && !isInitialized)
			{
				currentValue = this.SetValidValue<T>(currentValue, new T(), property);
			}
			return currentValue;
		}

		// Token: 0x06003923 RID: 14627
		internal abstract void ReportComplexPropertyChanging(string entityMemberName, ComplexObject complexObject, string complexMemberName);

		// Token: 0x06003924 RID: 14628
		internal abstract void ReportComplexPropertyChanged(string entityMemberName, ComplexObject complexObject, string complexMemberName);

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06003925 RID: 14629
		internal abstract bool IsChangeTracked { get; }

		// Token: 0x06003926 RID: 14630 RVA: 0x000BD11F File Offset: 0x000BB31F
		protected internal static bool BinaryEquals(byte[] first, byte[] second)
		{
			return first == second || (first != null && second != null && ByValueEqualityComparer.CompareBinaryValues(first, second));
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x000BD136 File Offset: 0x000BB336
		protected internal static byte[] GetValidValue(byte[] currentValue)
		{
			if (currentValue == null)
			{
				return null;
			}
			return (byte[])currentValue.Clone();
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x000BD148 File Offset: 0x000BB348
		protected internal static byte[] SetValidValue(byte[] value, bool isNullable, string propertyName)
		{
			if (value == null)
			{
				if (!isNullable)
				{
					EntityUtil.ThrowPropertyIsNotNullable(propertyName);
				}
				return value;
			}
			return (byte[])value.Clone();
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x000BD163 File Offset: 0x000BB363
		protected internal static byte[] SetValidValue(byte[] value, bool isNullable)
		{
			return StructuralObject.SetValidValue(value, isNullable, null);
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000BD16D File Offset: 0x000BB36D
		protected internal static bool SetValidValue(bool value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000BD170 File Offset: 0x000BB370
		protected internal static bool SetValidValue(bool value)
		{
			return value;
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000BD173 File Offset: 0x000BB373
		protected internal static bool? SetValidValue(bool? value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000BD176 File Offset: 0x000BB376
		protected internal static bool? SetValidValue(bool? value)
		{
			return value;
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000BD179 File Offset: 0x000BB379
		protected internal static byte SetValidValue(byte value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000BD17C File Offset: 0x000BB37C
		protected internal static byte SetValidValue(byte value)
		{
			return value;
		}

		// Token: 0x06003930 RID: 14640 RVA: 0x000BD17F File Offset: 0x000BB37F
		protected internal static byte? SetValidValue(byte? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003931 RID: 14641 RVA: 0x000BD182 File Offset: 0x000BB382
		protected internal static byte? SetValidValue(byte? value)
		{
			return value;
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000BD185 File Offset: 0x000BB385
		[CLSCompliant(false)]
		protected internal static sbyte SetValidValue(sbyte value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003933 RID: 14643 RVA: 0x000BD188 File Offset: 0x000BB388
		[CLSCompliant(false)]
		protected internal static sbyte SetValidValue(sbyte value)
		{
			return value;
		}

		// Token: 0x06003934 RID: 14644 RVA: 0x000BD18B File Offset: 0x000BB38B
		[CLSCompliant(false)]
		protected internal static sbyte? SetValidValue(sbyte? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003935 RID: 14645 RVA: 0x000BD18E File Offset: 0x000BB38E
		[CLSCompliant(false)]
		protected internal static sbyte? SetValidValue(sbyte? value)
		{
			return value;
		}

		// Token: 0x06003936 RID: 14646 RVA: 0x000BD191 File Offset: 0x000BB391
		protected internal static DateTime SetValidValue(DateTime value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003937 RID: 14647 RVA: 0x000BD194 File Offset: 0x000BB394
		protected internal static DateTime SetValidValue(DateTime value)
		{
			return value;
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x000BD197 File Offset: 0x000BB397
		protected internal static DateTime? SetValidValue(DateTime? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003939 RID: 14649 RVA: 0x000BD19A File Offset: 0x000BB39A
		protected internal static DateTime? SetValidValue(DateTime? value)
		{
			return value;
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000BD19D File Offset: 0x000BB39D
		protected internal static TimeSpan SetValidValue(TimeSpan value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600393B RID: 14651 RVA: 0x000BD1A0 File Offset: 0x000BB3A0
		protected internal static TimeSpan SetValidValue(TimeSpan value)
		{
			return value;
		}

		// Token: 0x0600393C RID: 14652 RVA: 0x000BD1A3 File Offset: 0x000BB3A3
		protected internal static TimeSpan? SetValidValue(TimeSpan? value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000BD1A6 File Offset: 0x000BB3A6
		protected internal static TimeSpan? SetValidValue(TimeSpan? value)
		{
			return value;
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000BD1A9 File Offset: 0x000BB3A9
		protected internal static DateTimeOffset SetValidValue(DateTimeOffset value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000BD1AC File Offset: 0x000BB3AC
		protected internal static DateTimeOffset SetValidValue(DateTimeOffset value)
		{
			return value;
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000BD1AF File Offset: 0x000BB3AF
		protected internal static DateTimeOffset? SetValidValue(DateTimeOffset? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x000BD1B2 File Offset: 0x000BB3B2
		protected internal static DateTimeOffset? SetValidValue(DateTimeOffset? value)
		{
			return value;
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000BD1B5 File Offset: 0x000BB3B5
		protected internal static decimal SetValidValue(decimal value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x000BD1B8 File Offset: 0x000BB3B8
		protected internal static decimal SetValidValue(decimal value)
		{
			return value;
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x000BD1BB File Offset: 0x000BB3BB
		protected internal static decimal? SetValidValue(decimal? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x000BD1BE File Offset: 0x000BB3BE
		protected internal static decimal? SetValidValue(decimal? value)
		{
			return value;
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000BD1C1 File Offset: 0x000BB3C1
		protected internal static double SetValidValue(double value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x000BD1C4 File Offset: 0x000BB3C4
		protected internal static double SetValidValue(double value)
		{
			return value;
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x000BD1C7 File Offset: 0x000BB3C7
		protected internal static double? SetValidValue(double? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000BD1CA File Offset: 0x000BB3CA
		protected internal static double? SetValidValue(double? value)
		{
			return value;
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x000BD1CD File Offset: 0x000BB3CD
		protected internal static float SetValidValue(float value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000BD1D0 File Offset: 0x000BB3D0
		protected internal static float SetValidValue(float value)
		{
			return value;
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x000BD1D3 File Offset: 0x000BB3D3
		protected internal static float? SetValidValue(float? value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x000BD1D6 File Offset: 0x000BB3D6
		protected internal static float? SetValidValue(float? value)
		{
			return value;
		}

		// Token: 0x0600394E RID: 14670 RVA: 0x000BD1D9 File Offset: 0x000BB3D9
		protected internal static Guid SetValidValue(Guid value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x000BD1DC File Offset: 0x000BB3DC
		protected internal static Guid SetValidValue(Guid value)
		{
			return value;
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x000BD1DF File Offset: 0x000BB3DF
		protected internal static Guid? SetValidValue(Guid? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000BD1E2 File Offset: 0x000BB3E2
		protected internal static Guid? SetValidValue(Guid? value)
		{
			return value;
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x000BD1E5 File Offset: 0x000BB3E5
		protected internal static short SetValidValue(short value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x000BD1E8 File Offset: 0x000BB3E8
		protected internal static short SetValidValue(short value)
		{
			return value;
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000BD1EB File Offset: 0x000BB3EB
		protected internal static short? SetValidValue(short? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x000BD1EE File Offset: 0x000BB3EE
		protected internal static short? SetValidValue(short? value)
		{
			return value;
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x000BD1F1 File Offset: 0x000BB3F1
		protected internal static int SetValidValue(int value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x000BD1F4 File Offset: 0x000BB3F4
		protected internal static int SetValidValue(int value)
		{
			return value;
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x000BD1F7 File Offset: 0x000BB3F7
		protected internal static int? SetValidValue(int? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000BD1FA File Offset: 0x000BB3FA
		protected internal static int? SetValidValue(int? value)
		{
			return value;
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000BD1FD File Offset: 0x000BB3FD
		protected internal static long SetValidValue(long value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x000BD200 File Offset: 0x000BB400
		protected internal static long SetValidValue(long value)
		{
			return value;
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000BD203 File Offset: 0x000BB403
		protected internal static long? SetValidValue(long? value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x000BD206 File Offset: 0x000BB406
		protected internal static long? SetValidValue(long? value)
		{
			return value;
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000BD209 File Offset: 0x000BB409
		[CLSCompliant(false)]
		protected internal static ushort SetValidValue(ushort value, string propertyName)
		{
			return value;
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x000BD20C File Offset: 0x000BB40C
		[CLSCompliant(false)]
		protected internal static ushort SetValidValue(ushort value)
		{
			return value;
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x000BD20F File Offset: 0x000BB40F
		[CLSCompliant(false)]
		protected internal static ushort? SetValidValue(ushort? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x000BD212 File Offset: 0x000BB412
		[CLSCompliant(false)]
		protected internal static ushort? SetValidValue(ushort? value)
		{
			return value;
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x000BD215 File Offset: 0x000BB415
		[CLSCompliant(false)]
		protected internal static uint SetValidValue(uint value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x000BD218 File Offset: 0x000BB418
		[CLSCompliant(false)]
		protected internal static uint SetValidValue(uint value)
		{
			return value;
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x000BD21B File Offset: 0x000BB41B
		[CLSCompliant(false)]
		protected internal static uint? SetValidValue(uint? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x000BD21E File Offset: 0x000BB41E
		[CLSCompliant(false)]
		protected internal static uint? SetValidValue(uint? value)
		{
			return value;
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x000BD221 File Offset: 0x000BB421
		[CLSCompliant(false)]
		protected internal static ulong SetValidValue(ulong value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x000BD224 File Offset: 0x000BB424
		[CLSCompliant(false)]
		protected internal static ulong SetValidValue(ulong value)
		{
			return value;
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x000BD227 File Offset: 0x000BB427
		[CLSCompliant(false)]
		protected internal static ulong? SetValidValue(ulong? value, string propertyName)
		{
			return value;
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x000BD22A File Offset: 0x000BB42A
		[CLSCompliant(false)]
		protected internal static ulong? SetValidValue(ulong? value)
		{
			return value;
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x000BD22D File Offset: 0x000BB42D
		protected internal static string SetValidValue(string value, bool isNullable, string propertyName)
		{
			if (value == null && !isNullable)
			{
				EntityUtil.ThrowPropertyIsNotNullable(propertyName);
			}
			return value;
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000BD23C File Offset: 0x000BB43C
		protected internal static string SetValidValue(string value, bool isNullable)
		{
			return StructuralObject.SetValidValue(value, isNullable, null);
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000BD246 File Offset: 0x000BB446
		protected internal static DbGeography SetValidValue(DbGeography value, bool isNullable, string propertyName)
		{
			if (value == null && !isNullable)
			{
				EntityUtil.ThrowPropertyIsNotNullable(propertyName);
			}
			return value;
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000BD255 File Offset: 0x000BB455
		protected internal static DbGeography SetValidValue(DbGeography value, bool isNullable)
		{
			return StructuralObject.SetValidValue(value, isNullable, null);
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000BD25F File Offset: 0x000BB45F
		protected internal static DbGeometry SetValidValue(DbGeometry value, bool isNullable, string propertyName)
		{
			if (value == null && !isNullable)
			{
				EntityUtil.ThrowPropertyIsNotNullable(propertyName);
			}
			return value;
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000BD26E File Offset: 0x000BB46E
		protected internal static DbGeometry SetValidValue(DbGeometry value, bool isNullable)
		{
			return StructuralObject.SetValidValue(value, isNullable, null);
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000BD278 File Offset: 0x000BB478
		protected internal T SetValidValue<T>(T oldValue, T newValue, string property) where T : ComplexObject
		{
			if (newValue == null && this.IsChangeTracked)
			{
				throw new InvalidOperationException(Strings.ComplexObject_NullableComplexTypesNotSupported(property));
			}
			if (oldValue != null)
			{
				oldValue.DetachFromParent();
			}
			if (newValue != null)
			{
				newValue.AttachToParent(this, property);
			}
			return newValue;
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000BD2CA File Offset: 0x000BB4CA
		protected internal static TComplex VerifyComplexObjectIsNotNull<TComplex>(TComplex complexObject, string propertyName) where TComplex : ComplexObject
		{
			if (complexObject == null)
			{
				EntityUtil.ThrowPropertyIsNotNullable(propertyName);
			}
			return complexObject;
		}

		// Token: 0x04001306 RID: 4870
		public const string EntityKeyPropertyName = "-EntityKey-";
	}
}
