public class BossFactory {
    public BossDeath BossDeath;
    public BossAttack BossAttack;
    public BossWalk BossWalk;
    public BossSpecialAttack BossSpecialAttack;
    
    public BossFactory(BossController context, BossStateMachine stateMachine) {
        BossAttack = new BossAttack(context, stateMachine, this);
        BossWalk = new BossWalk(context, stateMachine, this);
        BossDeath = new BossDeath(context, stateMachine, this);
        BossSpecialAttack = new BossSpecialAttack(context, stateMachine, this);
    }
}