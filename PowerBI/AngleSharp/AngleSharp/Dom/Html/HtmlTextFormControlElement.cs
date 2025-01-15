using System;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003A2 RID: 930
	internal abstract class HtmlTextFormControlElement : HtmlFormControlElementWithState
	{
		// Token: 0x06001D34 RID: 7476 RVA: 0x0005563B File Offset: 0x0005383B
		public HtmlTextFormControlElement(Document owner, string name, string prefix, NodeFlags flags = NodeFlags.None)
			: base(owner, name, prefix, flags)
		{
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x00055648 File Offset: 0x00053848
		// (set) Token: 0x06001D36 RID: 7478 RVA: 0x00055650 File Offset: 0x00053850
		public bool IsDirty
		{
			get
			{
				return this._dirty;
			}
			set
			{
				this._dirty = value;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x00055659 File Offset: 0x00053859
		// (set) Token: 0x06001D38 RID: 7480 RVA: 0x00055666 File Offset: 0x00053866
		public string DirectionName
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.DirName);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.DirName, value, false);
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x00055675 File Offset: 0x00053875
		// (set) Token: 0x06001D3A RID: 7482 RVA: 0x00055688 File Offset: 0x00053888
		public int MaxLength
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.MaxLength).ToInteger(-1);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.MaxLength, value.ToString(), false);
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0005569D File Offset: 0x0005389D
		// (set) Token: 0x06001D3C RID: 7484 RVA: 0x000556B0 File Offset: 0x000538B0
		public int MinLength
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.MinLength).ToInteger(0);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.MinLength, value.ToString(), false);
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001D3D RID: 7485
		// (set) Token: 0x06001D3E RID: 7486
		public abstract string DefaultValue { get; set; }

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x000556C5 File Offset: 0x000538C5
		public bool HasValue
		{
			get
			{
				return this._value != null || this.HasOwnAttribute(AttributeNames.Value);
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x000556DC File Offset: 0x000538DC
		// (set) Token: 0x06001D41 RID: 7489 RVA: 0x000556EE File Offset: 0x000538EE
		public string Value
		{
			get
			{
				return this._value ?? this.DefaultValue;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x000556F7 File Offset: 0x000538F7
		// (set) Token: 0x06001D43 RID: 7491 RVA: 0x00055704 File Offset: 0x00053904
		public string Placeholder
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Placeholder);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Placeholder, value, false);
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0005458A File Offset: 0x0005278A
		// (set) Token: 0x06001D45 RID: 7493 RVA: 0x00054597 File Offset: 0x00052797
		public bool IsRequired
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Required);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Required, value);
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001D46 RID: 7494 RVA: 0x00055713 File Offset: 0x00053913
		// (set) Token: 0x06001D47 RID: 7495 RVA: 0x00055720 File Offset: 0x00053920
		public bool IsReadOnly
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Readonly);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Readonly, value);
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x0005572E File Offset: 0x0005392E
		// (set) Token: 0x06001D49 RID: 7497 RVA: 0x00055736 File Offset: 0x00053936
		public int SelectionStart
		{
			get
			{
				return this._start;
			}
			set
			{
				this.SetSelectionRange(value, this._end, this._direction);
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x0005574B File Offset: 0x0005394B
		// (set) Token: 0x06001D4B RID: 7499 RVA: 0x00055753 File Offset: 0x00053953
		public int SelectionEnd
		{
			get
			{
				return this._end;
			}
			set
			{
				this.SetSelectionRange(this._start, value, this._direction);
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x00055768 File Offset: 0x00053968
		public string SelectionDirection
		{
			get
			{
				return this._direction.ToString().ToLowerInvariant();
			}
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x00055780 File Offset: 0x00053980
		public override INode Clone(bool deep = true)
		{
			HtmlTextFormControlElement htmlTextFormControlElement = (HtmlTextFormControlElement)base.Clone(deep);
			htmlTextFormControlElement._dirty = this._dirty;
			htmlTextFormControlElement._value = this._value;
			htmlTextFormControlElement._direction = this._direction;
			htmlTextFormControlElement._start = this._start;
			htmlTextFormControlElement._end = this._end;
			return htmlTextFormControlElement;
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x000557D5 File Offset: 0x000539D5
		public void Select(int selectionStart, int selectionEnd, string selectionDirection = null)
		{
			this.SetSelectionRange(selectionStart, selectionEnd, selectionDirection.ToEnum(HtmlTextFormControlElement.SelectionType.Forward));
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x000557E6 File Offset: 0x000539E6
		public void SelectAll()
		{
			this.SetSelectionRange(0, this.Value.Length, HtmlTextFormControlElement.SelectionType.Forward);
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x000557FC File Offset: 0x000539FC
		protected override void Check(ValidityState state)
		{
			int length = (this.Value ?? string.Empty).Length;
			int maxLength = this.MaxLength;
			int minLength = this.MinLength;
			state.IsValueMissing = this.IsRequired && length == 0;
			state.IsTooLong = this._dirty && maxLength > -1 && length > maxLength;
			state.IsTooShort = this._dirty && length > 0 && length < minLength;
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x00055873 File Offset: 0x00053A73
		protected override bool CanBeValidated()
		{
			return !this.IsReadOnly && !this.HasDataListAncestor();
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x00055888 File Offset: 0x00053A88
		protected void ConstructDataSet(FormDataSet dataSet, string type)
		{
			dataSet.Append(base.Name, this.Value, type);
			string ownAttribute = this.GetOwnAttribute(AttributeNames.DirName);
			if (!string.IsNullOrEmpty(ownAttribute))
			{
				dataSet.Append(ownAttribute, base.Direction.ToString().ToLowerInvariant(), "Direction");
			}
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x000558D8 File Offset: 0x00053AD8
		private void SetSelectionRange(int selectionStart, int selectionEnd, HtmlTextFormControlElement.SelectionType selectionType)
		{
			int length = (this.Value ?? string.Empty).Length;
			if (selectionEnd > length)
			{
				selectionEnd = length;
			}
			if (selectionEnd < selectionStart)
			{
				selectionStart = selectionEnd;
			}
			this._start = selectionStart;
			this._end = selectionEnd;
			this._direction = selectionType;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0005591D File Offset: 0x00053B1D
		internal override void Reset()
		{
			this.Value = null;
			this.Select(int.MaxValue, int.MaxValue, null);
		}

		// Token: 0x04000D02 RID: 3330
		private bool _dirty;

		// Token: 0x04000D03 RID: 3331
		private string _value;

		// Token: 0x04000D04 RID: 3332
		private HtmlTextFormControlElement.SelectionType _direction;

		// Token: 0x04000D05 RID: 3333
		private int _start;

		// Token: 0x04000D06 RID: 3334
		private int _end;

		// Token: 0x0200051E RID: 1310
		public enum SelectionType : byte
		{
			// Token: 0x0400128B RID: 4747
			None,
			// Token: 0x0400128C RID: 4748
			Forward,
			// Token: 0x0400128D RID: 4749
			Backward
		}
	}
}
