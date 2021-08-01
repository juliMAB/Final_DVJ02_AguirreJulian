using System.Collections;
using System.Collections.Generic;

public interface IKilleable

{
    void kill();
}
public interface IHitable
{
    void TakeDamage(int damage);

    void death();
}
