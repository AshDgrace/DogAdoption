using Sandbox;
public enum TeamType
{
	[Icon("🧑")]
	[Description("Players, turrets, and whatnots")]
	Player,

	[Icon("💩")]
	[Description("Snots, enemies, etc")]
	Snot,

	[Icon("🐶")]
	[Description("Doggos, very cute")]
	Doggo 
}



public sealed class UnitComponent : Component
{
/// <summary>
/// The name displayed for your unit (the snot)
/// </summary>
	[Property]
	public string Name { get; set; }

/// <summary>
/// Which team this unit will be in
/// </summary>
	[Property]
	public TeamType Team { get; set; }

/// <summary>
/// What max health this has 
/// </summary>
	[Property]
	[Range( 10f, 300f )]
	[Step ( 10f )]
	public float MaxHealth { get; set; } = 100f;

	[Property]
	public SkinnedModelRenderer ModelRenderer { get; set; }

	private float _health;
	public float Health
	{
		get 
		{ 
			return _health; 
		}

		set 
		{ 
			UpdateHealth( value ); 
		}
	}


	protected override void OnUpdate()
	{

	}

protected override void OnStart ()
	{
		_health = MaxHealth;
	}

protected override void OnFixedUpdate()
	{
		base.OnFixedUpdate();
	}

protected override void OnDestroy()
	{
		base.OnDestroy();
	}

[Button( "Hurt 10", "💥" )]
public void HurtDebug()
	{
		Damage( 10f );
	}

[Button( "Heal 10", "❤️" )]
public void HealDebug()
	{
		Damage( -10f );
	}

[Button( "Hurt a lot", "💥" )]
public void HurtLotDebug()
	{
		Damage( 30f );
	}


/// <summary>
/// Positive = hurt, Negative = heal
/// </summary>
/// <param name="damage"></param>
public void Damage( float damage )
	{
		Health -= damage;
	}

private void UpdateHealth( float newHealth )
	{
		var difference = newHealth - Health;
		_health = float.Clamp( newHealth, 0f, MaxHealth);

		if ( !ModelRenderer.IsValid() ) return;

		if ( difference < 0f )
		{
			var remappedDamage = MathX.Remap( -difference, 0f, MaxHealth, 0f, 100f );
			DamageAnimation( -difference );
		}

		var remappedHealth = MathX.Remap( Health, 0f, MaxHealth, 0f, 100f );
		ModelRenderer.Set( "health", remappedHealth );

		if ( Health <= 0f )
			ModelRenderer.Set( "dead", true );
	}

private void DamageAnimation( float damage )
	{
		ModelRenderer.Set( "damage", damage );
		ModelRenderer.Set( "hit", true );
	}
	
}