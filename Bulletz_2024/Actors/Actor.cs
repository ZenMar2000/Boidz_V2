using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boidz
{
    enum WeaponType
    {
        Default,
        TripleShoot
    }

    abstract class Actor : GameObject
    {
        // Variables
        protected Vector2 shootOffset;
        //protected BulletType bulletType;

        protected int energy;
        protected int maxEnergy;

        //protected WeaponType weaponType;
        protected Vector2 shootVel;

        public bool IsAlive { get { return energy > 0; } }
        public virtual int Energy { get => energy; set { energy = MathHelper.Clamp(value, 0, maxEnergy); } }

        public Actor(string texturePath, int textOffsetX = 0, int textOffsetY = 0, int spriteWidth = 0, int spriteHeight = 0) : base(texturePath, textOffsetX, textOffsetY, spriteWidth, spriteHeight)
        {
            RigidBody = new RigidBody(this);

            maxEnergy = 100;

            //Reset();
        }

        //public void ChangeWeapon(WeaponType weapon)
        //{
        //    weaponType = weapon;
        //}

        //protected virtual void Shoot()
        //{
        //    Bullet b;

        //    switch (weaponType)
        //    {
        //        case WeaponType.Default:
        //            b = BulletMngr.GetBullet(bulletType);

        //            if (b != null)
        //            {
        //                b.Shoot(sprite.position + shootOffset, shootVel, 0);
        //            }
        //            break;

        //        case WeaponType.TripleShoot:
        //            float x = (float)Math.Cos(tripleShootAngle);
        //            float y = (float)Math.Sin(tripleShootAngle);

        //            Vector2 bulletDirection = new Vector2(x, y);

        //            for (int i = 0; i < 3; i++)
        //            {
        //                b = BulletMngr.GetBullet(bulletType);

        //                if (b != null)
        //                {
        //                    b.Shoot(Position + shootOffset, bulletDirection.Normalized() * 1000.0f, tripleShootAngle - (i * tripleShootAngle));
        //                    bulletDirection.Y -= y;
        //                }
        //            }
        //            break;
        //    }
        //}

        public virtual void AddDamage(int dmg)
        {
            Energy -= dmg;

            if (Energy <= 0)
            {
                OnDie();
            }
        }

        public virtual void OnDie()
        {

        }
             

        public virtual void Reset()
        {
            Energy = maxEnergy;
        }
    }
}
