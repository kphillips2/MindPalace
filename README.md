# Using Git

Guidelines and help when using git.

## Adding new sofware

Keep all new software on a separate branch so that master is always releasable.
Also, always pull before continuing work on a shared branch.

### Creating a branch

Branch names are in slug case.

-> using git command line instructions
```
git branch [branch-name]
```

-> directly from the repository

![](https://raw.githubusercontent.com/kphillips2/MindPalace/master/branch_add.png)

### Switching branches

To switch between tasks. Push any unfinished work to the current branch and then switch to the corresponding new branch.
To save your changes locally without pushing to the branch use ``git stash``. Note: these changes will disapear until restored.
To restore stashed changes use ``git stash apply``.
To see any local changes to the current branch use ``git diff``.

-> using git command line instructions
```
git checkout [branch-name]
```

-> directly from the repository

![](https://raw.githubusercontent.com/kphillips2/MindPalace/master/branch_switching.png)

### Merging branches

Once reviewed/tested branches can be merged with master so that master always contains all finished tasks. This should be done from the repository.
