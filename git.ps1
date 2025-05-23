# git.ps1

param(
    [string]$CommitMessage
)

# If no message was passed as an argument, ask the user for one
if (-not $CommitMessage) {
    $CommitMessage = Read-Host "Enter commit message"
}

# Stage all changes
git add .

# Commit with the provided message
git commit -m "$CommitMessage"

# Push to origin main
git push origin main
