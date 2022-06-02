    class Weapon
    {
        private int _damage;
        private int _bullets;

        public void Fire(Player player)
        {
            player.ApplyDamage(_damage);

            if (_bullets <= 0)
            {
                Debug.log("Пастроны закончились");
            }

            _bullets--;
        }
    }

    class Player
    {
        private int _health;

        public int Health => _health;

        public void ApplyDamage(int damage)
        {
            if (Health <= 0)
            {
                Debug.log("Game Ower")
            }

            _health -= damage
        }
    }

    class Bot
    {
        private Weapon Weapon;

        public void OnSeePlayer(Player player)
        {
            Weapon.Fire(player);
        }
    }