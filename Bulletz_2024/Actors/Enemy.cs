using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Boidz
{

    //enum EnemyType
    //{
    //    Enemy01, EnemyBig, LAST
    //}

    //abstract class Enemy : Actor
    //{
    //    protected float nextShoot;
    //    public EnemyType Type { get; protected set; }
    //    public int Points { get; protected set; }

    //    public Enemy(string textureName) : base(textureName)
    //    {
    //        RigidBody.Type = RigidBodyType.Enemy;

    //        sprite.FlipX = true;

    //        speed = -200;
    //        RigidBody.Velocity.X = speed;

    //        nextShoot = RandomGenerator.GetRandomInt(1, 2);

    //        shootVel = new Vector2(-600.0f, 0.0f);
    //    }

    //    public override void Update()
    //    {
    //        if(IsActive)
    //        {
    //            if (sprite.position.X + HalfWidth < 0)
    //            {
    //                //SpawnMngr.RestoreEnemy(this);
    //            }
    //            else
    //            {
    //                nextShoot -= Game.DeltaTime;

    //                if (nextShoot <= 0)
    //                {
    //                    nextShoot = RandomGenerator.GetRandomFloat() * 2 + 1;
    //                    //Shoot();
    //                }
    //            }
    //        }
    //    }

    //    public override void OnDie()
    //    {
    //        //SpawnMngr.RestoreEnemy(this);
    //    }
    //}
}
