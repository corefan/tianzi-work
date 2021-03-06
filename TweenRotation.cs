using System;
using UnityEngine;

[AddComponentMenu("NGUI/Tween/Tween Rotation")]
public class TweenRotation : UITweener
{
	public Vector3 from;

	public Vector3 to;

	private Transform mTrans;

	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	[Obsolete("Use 'value' instead")]
	public Quaternion rotation
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	public Quaternion value
	{
		get
		{
			return this.cachedTransform.localRotation;
		}
		set
		{
			this.cachedTransform.localRotation = value;
		}
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		Vector3 zero = Vector3.zero;
		zero.x = Mathf.Lerp(this.from.x, this.to.x, factor);
		zero.y = Mathf.Lerp(this.from.y, this.to.y, factor);
		zero.z = Mathf.Lerp(this.from.z, this.to.z, factor);
		this.value = Quaternion.Euler(zero);
	}

	public static TweenRotation Begin(GameObject go, float duration, Quaternion rot)
	{
		TweenRotation tweenRotation = UITweener.Begin<TweenRotation>(go, duration);
		tweenRotation.from = tweenRotation.value.eulerAngles;
		tweenRotation.to = rot.eulerAngles;
		if (duration <= 0f)
		{
			tweenRotation.Sample(1f, true);
			tweenRotation.enabled = false;
		}
		return tweenRotation;
	}

	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue()
	{
		this.from = this.value.eulerAngles;
	}

	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue()
	{
		this.to = this.value.eulerAngles;
	}

	[ContextMenu("Assume value of 'From'")]
	private void SetCurrentValueToStart()
	{
		this.value = Quaternion.Euler(this.from);
	}

	[ContextMenu("Assume value of 'To'")]
	private void SetCurrentValueToEnd()
	{
		this.value = Quaternion.Euler(this.to);
	}
}
